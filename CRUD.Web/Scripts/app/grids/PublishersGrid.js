$(function () {
    $("#PublishersGrid").kendoGrid({
        height: 400,
        columns: [
            { field: "Name", title: "Publisher name" },
            { field: "Journals", title: "Journal(s)", editor: journalsMultiselectEditor, template: "#= JSON.stringify(Journals) #" },
            { field: "Books", title: "Book(s)", editor: booksMultiselectEditor, template: "#= JSON.stringify(Books) #" },
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
                        for (var i = 0; i < Data.Data.length; i++) {
                            if (Data.Data[i].BooksList != null) {
                                if (Data.Data[i].BooksList.length != 0) {
                                    Data.Data[i].Books = Data.Data[i].BooksList[0].Name;
                                    for (var j = 1; j < Data.Data[i].BooksList.length; j++) {
                                        Data.Data[i].Books += ", " + Data.Data[i].BooksList[j].Name;
                                    }
                                    Data.Data[i].Books += ";";
                                }

                                if (Data.Data[i].JournalsList.length != 0) {
                                    Data.Data[i].Journals = Data.Data[i].JournalsList[0].Name;
                                    for (var j = 1; j < Data.Data[i].JournalsList.length; j++) {
                                        Data.Data[i].Journals += ", " + Data.Data[i].JournalsList[j].Name;
                                    }
                                    Data.Data[i].Journals += ";";
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
                        Journals: { defaultValue: [] },
                        Books: { defaultValue: [] },
                    }
                }
            },
            batch: true,
            transport: {
                create: {
                    url: "/Publishers/Create",
                    type: "POST",
                    complete: function (e) {
                        $("#PublishersGrid").data("kendoGrid").dataSource.read();
                    }
                },
                read: {
                    url: "/Publishers/Read",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: "/Publishers/Update",
                    type: "POST",
                    complete: function (e) {
                        $("#PublishersGrid").data("kendoGrid").dataSource.read();
                    }
                },
                destroy: {
                    url: "/Publishers/Delete",
                    type: "POST"
                },
                parameterMap: function (data, operation) {
                    if (operation != "read") {
                        var responsePublisherViewModel = data.models[0];

                        responsePublisherViewModel.BooksListId = new Array;
                        responsePublisherViewModel.JournalsListId = new Array;
                        if (operation != "destroy") {
                            for (var i = 0; i < responsePublisherViewModel.Books.length; i++) {
                                responsePublisherViewModel.BooksListId[i] = responsePublisherViewModel.Books[i];
                            }

                            for (var i = 0; i < responsePublisherViewModel.Journals.length; i++) {
                                responsePublisherViewModel.JournalsListId[i] = responsePublisherViewModel.Journals[i];
                            }
                        }
                        return responsePublisherViewModel;
                    } else {
                        return { data: kendo.stringify(operation.data) }
                    }
                }
            }
        }
    });
});

function journalsMultiselectEditor(container, options) {
    $('<select data-value-primitive="true" required name="' + options.field + '"></select>')
        .appendTo(container)
        .kendoMultiSelect({
            placeholder: "Pls, select journal(s)",
            filter: "contains",
            width: "100px",
            dataTextField: "Name",
            dataValueField: "Id",
            dataSource: {
                transport: {
                    read: {
                        url: "/Journals/ReadJournalsForMultiselect",
                        dataType: "json",
                        type: "GET"
                    }
                }},
        });
}

function booksMultiselectEditor(container, options) {
    $('<select data-value-primitive="true" required name="' + options.field + '"></select>')
        .appendTo(container)
        .kendoMultiSelect({
            placeholder: "Pls, select book(s)",
            filter: "contains",
            width: "100px",
            dataTextField: "Name",
            dataValueField: "Id",
            dataSource: {
                transport: {
                    read: {
                        url: "/Books/ReadBooksForMultiselect",
                        dataType: "json",
                        type: "GET"
                    }
                }},
        });
}