// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#msg-box").fadeOut(2500);
});

function ajaxModal() {
    $(document).ready(function () {
        $.ajaxSetup({ cache: false });

        $('a[data-modal]').on('click', function (e) {
            $('#myModalContent').load(this.href, function () {
                $('#myModal').modal({ keyboard: true }, 'show');
                bindForm(this);
            });

            return false;
        });
    });

    function bindForm(dialog) {
        $('form', dialog).submit(function () {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    if (result.success) {
                        $('#myModal').modal('hide');
                        $('#addressTarget').load(result.url);
                    } else {
                        $('#myModalContent').html(result);
                        bindForm(dialog);
                    }
                }
            });
            return false;
        });
    }
}

function cepSearch() {
    $(document).ready(function () {
        function cleanForm() {
            $("#Address_Street").val("");
            $("#Address_Number").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        $("#Address_ZipCode").blur(function () {
            // Get only number
            var zipCode = $(this).val().replace(/\D/g, "");

            if (zipCode !== "") {
                var zipCodeIsValid = /^[0-9]{8}$/;

                // While browsing webservices
                if (zipCodeIsValid.test(zipCode)) {
                    $("#Address_Street").val("...");
                    $("#Address_Number").val("...");
                    $("#Address_City").val("...");
                    $("#Address_State").val("...");

                    // Query webservices
                    $.getJSON("https://viacep.com.br/ws/" + zipCode + "/json/?callback=?",
                        function (result) {
                            if (!("erro" in result)) {
                                $("#Address_Street").val(result.logradouro + ", " + result.bairro);
                                $("#Address_Number").val(result.complemento);
                                $("#Address_City").val(result.localidade);
                                $("#Address_State").val(result.uf);
                            } else {
                                cleanForm();
                                alert("Not Found Address");
                            }
                        }
                    );
                } else {
                    cleanForm();
                    alert("Invalid ZipCode!");
                }
            } else {
                // Empty ZipCode
                cleanForm();
            }
        });
    });
}
