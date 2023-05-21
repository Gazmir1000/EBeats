$(document).ready(function () {
  

    function populateTable(categories) {
        var table = $("#categoryTable tbody");
        table.empty();

        $.each(categories, function (index, category) {
            var row = $("<tr>");
            row.append($("<td>").text(category.name));

            var actions = $("<td>");
            var editBtn = $("<button>")
                .addClass("btn btn-sm btn-primary mr-2")
                .text("Edit");
            var deleteBtn = $("<button>")
                .addClass("btn btn-sm btn-danger")
                .text("Delete");

            editBtn.click(function () {
                $("#categoryModalLabel").text("Edit Category");
                $("#categoryForm")[0].reset();
                $("#categoryForm").data("id", category.id);
                $("#name").val(category.name);
                $("#categoryModal").modal("show");
            });

            deleteBtn.click(function () {
                if (confirm("Are you sure you want to delete this category?")) {
                    $.ajax({
                        url: "https://localhost:7142/api/CategoryApi/" + category.id,
                        method: "DELETE",
                        success: function (response) {
                            refreshTable();
                        },
                    });
                }
            });

            actions.append(editBtn);
            actions.append(deleteBtn);
            row.append(actions);

            table.append(row);
        });
    }

    function refreshTable() {
        $.getJSON("https://localhost:7142/api/CategoryApi", function (categories) {
            populateTable(categories);
        });
    }

    const getCategories = () => {
        $.ajax(
            "https://localhost:7142/api/CategoryApi", // request url
            {
                success: function (data, status, xhr) {
                    // success callback function

                    if (data.length > 0) {
                        populateTable(data);
                    }
               
                },
            }
        );
    };
    getCategories();
    $("#addCategoryBtn").click(function () {
        $("#categoryModalLabel").text("Add Category");
        $("#categoryForm")[0].reset();
        $("#categoryModal").modal("show");
    });

    $("#cancelCategoryBtn").click(function(){
        $("#categoryModal").modal("hide");
    })
    $("#cancelCategoryBtnIcon").click(function(){
        $("#categoryModal").modal("hide");
    })

    // Save Category button click event
    $("#saveCategoryBtn").click(function () {
        var categoryData = {
            name: $("#name").val(),
            products:[],
        };

        // Create or Edit Category
        var categoryID = $("#categoryForm").data("id");
        if (categoryID) {
            // Edit existing category
            $.ajax({
                url: "https://localhost:7142/api/CategoryApi/" + categoryID,
                method: "PUT",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(categoryData), 
                success: function (response) {
                    $("#categoryModal").modal("hide");
                    refreshTable();
                },
            });
        } else {
            // Add new category
            $.ajax({
                url: "https://localhost:7142/api/CategoryApi",
                method: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(categoryData),
                success: function (response) {
                    $("#categoryModal").modal("hide");
                    refreshTable();
                },
            });
        }
    });

    $("#createForm").submit(function (event) {
        event.preventDefault();

        var name = $("#name").val();

        var data = {
            name: name,
            products: [],
        };

        $.ajax({
            url: "https://localhost:7142/api/CategoryApi",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (data) {
                console.log("hello");
                getCategories();
            },
            error: function () {
                alert("An error occurred while creating the product.");
            },
        });
    });
});
