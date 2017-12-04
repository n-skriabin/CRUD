$(function () {
    $("#BooksGrid").kendoGrid({
        height: 400,
        columns: [
            { field: "Name", title: "Book name" },
            { field: "Year", title: "Date" },
            { field: "Author", title: "Author(s)", editor: authorsMultiselectEditor, template: "#= JSON.stringify(Author) #" },
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
                            if (Data.Data[i].AuthorsList != null) {
                                if (Data.Data[i].AuthorsList.length != 0) {
                                    console.log(Data.Data);
                                    Data.Data[i].Author = Data.Data[i].AuthorsList[0].Abbreviated;
                                    for (var j = 1; j < Data.Data[i].AuthorsList.length; j++) {
                                        Data.Data[i].Author += ", " + Data.Data[i].AuthorsList[j].Abbreviated;
                                    }
                                    Data.Data[i].Author += ";";
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
                        Author: { defaultValue: [] },
                    }
                }
            },
            batch: true,
            transport: {
                create: {
                    url: "/Books/Create",
                    type: "POST",
                    complete: function (e) {
                        $("#BooksGrid").data("kendoGrid").dataSource.read();
                    }
                },
                read: {
                    url: "/Books/Read",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: "/Books/Update",
                    type: "POST",
                    complete: function (e) {
                        $("#BooksGrid").data("kendoGrid").dataSource.read();
                    }
                },
                destroy: {
                    url: "/Books/Delete",
                    type: "POST"
                },
                parameterMap: function (Data, operation) {
                    if (operation != "read") {
                        console.log(operation);
                        var responseBookViewModel = Data.models[0];
                        responseBookViewModel.AuthorsList = new Array;
                        if (operation != "destroy") {
                            for (var i = 0; i < responseBookViewModel.Author.length; i++) {
                                responseBookViewModel.AuthorsList[i] = responseBookViewModel.Author[i];
                            }
                        }
                        console.log(responseBookViewModel);
                        return responseBookViewModel;
                    }

                    if (operation == "read") {
                        return { data: kendo.stringify(operation.data) }
                    }
                }
            }
        }
    });
});

function authorsMultiselectEditor(container, options) {
    $('<select data-value-primitive="true" required name="' + options.field + '"></select>')
        .appendTo(container)
        .kendoMultiSelect({
            placeholder: "Pls, select a authors",
            filter: "contains",
            width: "100px",
            dataTextField: "Abbreviated",
            dataValueField: "Id",
            dataSource: {
                transport: {
                    read: {
                        url: "/Authors/ReadAuthorsForDropDown",
                        dataType: "json",
                        type: "GET"
                    }
                },
            }
        });
}
   