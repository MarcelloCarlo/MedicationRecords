$(function () {

    DecimalOnly($("#txtDosage"));
    NameOnly($("#txtDosage"));
    NameOnly($("#txtPatients"));

    var successMessage = '';
    //Added Anti-forgery Token
    var token = $('input[name="__RequestVerificationToken"]').val();
    var headers = {};
    headers['__RequestVerificationToken'] = token;
    var btnDismiss = '<button type="button" class="btn btn-primary" data-dismiss="modal" id="btnClose">Ok</button>';

    if (pageName == 'Create') {
        $("#modalContent").html('');
        $("#modalContent").append('<label>Are you sure you want to add this record?</label>');
        successMessage = 'Record successfully saved.';
    }

    if (pageName == 'Edit') {
        $("#modalContent").html('');
        $("#modalContent").append('<label>Are you sure you want to update this record?</label>');
        successMessage = 'Record successfully updated.';
    }

    if (pageName == 'Index') {
        $("#modalContent").html('');
        $("#modalContent").append('<label>Are you sure you want to delete this record?</label>');
        successMessage = 'Record successfully deleted.';
    }

    $("#btnSubmit").click(function () {

        //On submit
        $("#btnConfirm").click(function () {

            var inputVal = {
                Id: (patientId == '' || patientId == null ? '0' : patientId),
                Dosage: $("#txtDosage").val().trim(),
                Drug: $("#txtDrug").val().trim(),
                Patients: $("#txtPatients").val().trim()
            };

            $.ajax({
                url: window.submitFormURL,
                type: "POST",
                data: JSON.stringify(inputVal),
                headers: headers,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.IsSuccess) {

                        $("#modalFooter").html('')
                        $("#modalFooter").append(btnDismiss);
                        $("#modalContent").html('');
                        $("#modalContent").append('<label>' + successMessage + '</label>');

                        $("#btnClose").click(function () {
                            setTimeout(function () {
                                window.location.replace("/Medication");
                            }, 1000);
                        });

                    } else {
                        if (data.IsListResult) {
                            var msg = "";

                            for (var i = 0; i < data.Result.length; i++) {
                                msg += "Error : " + data.Result[i] + "\n";
                            }

                            $("#modalFooter").html('')
                            $("#modalFooter").append(btnDismiss);
                            $("#modalContent").html('');
                            $("#modalContent").append('<label>' + msg + '</label>');

                            $("#btnClose").click(function () {
                                setTimeout(function () {
                                    window.location.replace("/Medication");
                                }, 1000);
                            });
                        }
                        else {

                            $("#modalFooter").html('')
                            $("#modalFooter").append(btnDismiss);
                            $("#modalContent").html('');
                            $("#modalContent").append('<label> Error: ' + data.Result + '</label>');

                            $("#btnClose").click(function () {
                                setTimeout(function () {
                                    window.location.replace("/Medication");
                                }, 1000);
                            });
                        }
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#modalFooter").html('')
                    $("#modalFooter").append(btnDismiss);
                    $("#modalContent").html('');
                    $("#modalContent").append('<label> Error: ' + $(jqXHR.responseText).filter('title').text() + ', ' + textStatus + ', ' + errorThrown + '</label>');

                    $("#btnClose").click(function () {
                        setTimeout(function () {
                            window.location.replace("/Medication");
                        }, 1000);
                    });
                },
            });
        });

    });

    $('a[id^="btnDelete"]').click(function () {

        var selectedId = $(this).attr('value');

        $("#btnConfirm").click(function () {

            var inputVal = {
                Id: selectedId
            };

            $.ajax({
                url: window.submitFormURL,
                type: "POST",
                data: JSON.stringify(inputVal),
                headers: headers,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.IsSuccess) {

                        $("#modalFooter").html('')
                        $("#modalFooter").append(btnDismiss);
                        $("#modalContent").html('');
                        $("#modalContent").append('<label>' + successMessage + '</label>');

                        $("#btnClose").click(function () {
                            setTimeout(function () {
                                window.location.replace("/Medication");
                            }, 1000);
                        });

                    } else {
                        if (data.IsListResult) {
                            var msg = "";

                            for (var i = 0; i < data.Result.length; i++) {
                                msg += "Error : " + data.Result[i] + "\n";
                            }

                            $("#modalFooter").html('')
                            $("#modalFooter").append(btnDismiss);
                            $("#modalContent").html('');
                            $("#modalContent").append('<label>' + msg + '</label>');

                            $("#btnClose").click(function () {
                                setTimeout(function () {
                                    window.location.replace("/Medication");
                                }, 1000);
                            });
                        }
                        else {

                            $("#modalFooter").html('')
                            $("#modalFooter").append(btnDismiss);
                            $("#modalContent").html('');
                            $("#modalContent").append('<label> Error: ' + data.Result + '</label>');

                            $("#btnClose").click(function () {
                                setTimeout(function () {
                                    window.location.replace("/Medication");
                                }, 1000);
                            });
                        }
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#modalFooter").html('')
                    $("#modalFooter").append(btnDismiss);
                    $("#modalContent").html('');
                    $("#modalContent").append('<label> Error: ' + $(jqXHR.responseText).filter('title').text() + ', ' + textStatus + ', ' + errorThrown + '</label>');

                    $("#btnClose").click(function () {
                        setTimeout(function () {
                            window.location.replace("/Medication");
                        }, 1000);
                    });
                },
            });

            return false;
        });
    });

    $("#btnClear").click(function () {
        $("#txtDosage").val('');
        $("#txtDrug").val('');
        $("#txtPatients").val('');
    });


});

function FormValidation() {

    jQuery.validator.addMethod("DecimalFormat", function (value, element, params) {
        return this.optional(element) || /^\d{0,3}(\.\d{0,4})?$/i.test(value);
    }, false);

}

function DecimalOnly(textbox) {
    textbox.keydown(function (e) {
        if ("" + /^[^0-9.]$/.test(e.key) == "true") {
            e.preventDefault();
        }
    });
}

function NameOnly(textbox) {
    textbox.keydown(function (e) {
        if ("" + /^[!@#$%^&*()+=`~?><,\"/\\:;\]\[{}|•√π÷×¶∆£¢€¥°©®™℅¡¿_]$/.test(e.key) == "true") {
            e.preventDefault();
        }
    });
}