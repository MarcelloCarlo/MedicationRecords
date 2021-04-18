$(function () {
    $("#btnClear").click(function () {
        $("#txtDosage").val('');
        $("#txtDrug").val('');
        $("#txtPatients").val('');
    });

    var successMessage = '';
    $("#btnSubmit").click(function () {
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

        //Added Anti-forgery Token
        var token = $('input[name="__RequestVerificationToken"]').val();
        var headers = {};
        headers['__RequestVerificationToken'] = token;

        var btnDismiss = '<button type="button" class="btn btn-primary" data-dismiss="modal" id="btnClose">Ok</button>';
        //On submit
        $("#btnConfirm").click(function () {

            var inputVal = {
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

                        $("#confirmationModal").modal('hide');
                        $("#modalContent").html('');
                        $("#modalContent").append('<label>' + successMessage + '</label>');
                        $("#messageModal").modal('show');

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
                    $("#modalContent").append('<label> Error: ' + $(jqXHR.responseText).filter('title').text() + textStatus + errorThrown + '</label>');

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
});