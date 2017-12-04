$(function () {
    $("#ArticlesGrid").kendoGrid({
        height: 400,
        columns: [
            { field: "Name", title: "Article name" },
            { field: "Year", title: "Date" },
            { field: "AuthorID", title: "Author", editor: authorsDropDownEditor, template: "#= JSON.stringify(data.Abbreviated) #" },
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
                data: "Data",
                total: "Total",
                model: {
                    id: "Id",
                    fields: {
                        Id: { validation: { required: false, editable: false } },
                        Name: { validation: { required: true } },
                        Year: { validation: { required: true } },
                        Abbreviated: {},
                        AuthorId: {},
                    }
                }
            },
            batch: true,
            transport: {
                create: {
                    url: "/Articles/Create",
                    type: "POST",
                    complete: function (e) {
                        $("#ArticlesGrid").data("kendoGrid").dataSource.read();
                    }
                },
                read: {
                    url: "/Articles/Read",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: "/Articles/Update",
                    type: "POST",
                    complete: function (e) {
                        $("#ArticlesGrid").data("kendoGrid").dataSource.read();
                    }
                },
                destroy: {
                    url: "/Articles/Delete",
                    type: "POST"
                },
                parameterMap: function (data, operation) {
                    if (operation != "read") {
                        var articleViewModel = data.models[0];
                        return articleViewModel;
                    } else {
                        return { data: kendo.stringify(operation.data) }
                    }
                }
            }
        }
    });
});

function authorsDropDownEditor(container, options) {
    $('<input required name="' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            placeholder: "Pls, select a author",
            autoBind: false,
            dataTextField: "Abbreviated",
            dataValueField: "Id",
            dataSource: {
                type: "data",
                transport: {
                    read: "/Authors/ReadAuthorsForDropDown",
                    dataType: "json",
                    type: "GET"
                }
            }
        });
}
