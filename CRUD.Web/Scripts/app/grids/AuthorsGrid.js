$(function () {

    $("#AuthorsGrid").kendoGrid({
        height: 400,
        columns: [
            { field: "FirstName", title: "First name" },
            { field: "Patronymic", title: "Patronymic(or pseudonym)" },
            { field: "LastName", title: "Last name" },
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
                        FirstName: { validation: { required: true } },
                        LastName: { validation: { required: true } },
                        Patronymic: { validation: { required: false } },
                    }
                }
            },
            batch: true,
            transport: {
                create: {
                    url: "/Authors/Create",
                    dataType: "json",
                    type: "POST"
                },
                read: {
                    url: "/Authors/Read",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: "/Authors/Update",
                    dataType: "json",
                    type: "POST"
                },
                destroy: {
                    url: "/Authors/Delete",
                    dataType: "json",
                    type: "POST"
                },
                parameterMap: function (data, operation) {
                    if (operation != "read") {
                        var authorViewModel = data.models[0];
                        return authorViewModel;
                    } else {
                        return { data: kendo.stringify(operation.data) }
                    }
                }
            }
        }
    });
});