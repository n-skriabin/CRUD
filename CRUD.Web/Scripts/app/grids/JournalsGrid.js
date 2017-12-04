$(function () {
    var grid = $("#JournalsGrid").kendoGrid({
        height: 400,
        columns: [
            { field: "Name", title: "Journal name" },
            { field: "Date", title: "Date" },
            { field: "Articles", title: "Article(s)", editor: articlesMultiselectEditor, template: "#= JSON.stringify(Articles) #" },
            { command: ["edit", "destroy"], title: "Actions", width: "200px" }
        ],
        editable: "popup",
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        sortable: false,
        filterable: false,
        toolbar: ["create"],
        dataSource: {
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
            pageSize: 10,
            schema: {
                parse: function (Data) {
                    if (Data.Data != undefined) {
                        console.log(Data.Data);
                        for (var i = 0; i < Data.Data.length; i++) {
                            if (Data.Data[i].ArticlesList != null) {
                                if (Data.Data[i].ArticlesList.length != 0) {
                                    console.log(Data.Data);
                                    Data.Data[i].Articles = Data.Data[i].ArticlesList[0].Name;
                                    for (var j = 1; j < Data.Data[i].ArticlesList.length; j++) {
                                        Data.Data[i].Articles += ", " + Data.Data[i].ArticlesList[j].Name;
                                    }
                                    Data.Data[i].Articles += ";";
                                }
                            }
                        }
                    }
                    return Data;                   
                },
                data: "Data",
                total: "Total",
                model: {
                    id: "Id",
                    fields: {
                        Id: { validation: { required: false, editable: false } },
                        Name: { validation: { required: true } },
                        Year: { validation: { required: true } },
                        Articles: { validation: { required: true } },
                    }
                }
            },
            batch: true,
            transport: {
                create: {
                    url: "/Journals/Create",
                    type: "POST",
                    complete: function (e) {
                        $("#JournalsGrid").data("kendoGrid").dataSource.read();
                    }
                },
                read: {
                    url: "/Journals/Read",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: "/Journals/Update",
                    type: "POST",
                    complete: function (e) {
                        $("#JournalsGrid").data("kendoGrid").dataSource.read();
                    }
                },
                destroy: {
                    url: "/Journals/Delete",
                    type: "POST"
                },
                parameterMap: function (Data, operation) {
                    if (operation != "read") {
                        console.log(operation);
                        var responseJournalViewModel = Data.models[0];
                        console.log(responseJournalViewModel);
                        responseJournalViewModel.ArticlesList = new Array;
                        if (operation != "destroy"){
                            for (var i = 0; i < responseJournalViewModel.Articles.length; i++) {
                                responseJournalViewModel.ArticlesList[i] = responseJournalViewModel.Articles[i];
                            }
                        }
                        console.log(responseJournalViewModel);
                        return responseJournalViewModel;
                    }

                    if (operation == "read") {
                        return { data: kendo.stringify(operation.data) }
                    }
                }
            }
        }
    });
});

function articlesMultiselectEditor(container, options) {
    $('<select data-value-primitive="true" required name="' + options.field + '"></select>')
        .appendTo(container)
        .kendoMultiSelect({
            placeholder: "Pls, select article(s)",
            filter: "contains",
            width: "100px",
            dataTextField: "Name",
            dataValueField: "Id",
            dataSource: {
                transport: {
                    read: {
                        url: "/Articles/ReadArticlesForMultiSelect",
                        dataType: "json",
                        type: "GET"
                    }
                }},
        });
}