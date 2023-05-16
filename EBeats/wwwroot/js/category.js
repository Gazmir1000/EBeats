$(document).ready(function () {
    const getCategories = () => {
        $.ajax('https://localhost:7142/api/CategoryApi',   // request url
            {
                success: function (data, status, xhr) {// success callback function
                    console.log(data)

                    $('#total-number-cont').append("$" + data)
                    $('#total-number-cont').append("")

                }
            });
    }
    getCategories()
 

    $('#createForm').submit(function (event) {
        event.preventDefault();

        var name = $('#name').val();

        var data = {
            name: name,
        };

        $.ajax({
            url: 'https://localhost:7142/api/CategoryApi',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (data) {
                console.log(data)
                $('#addCategoryModal').modal('hide');
                getCategories()
            },
            error: function () {
                alert('An error occurred while creating the product.');
            }
        });
    });

})