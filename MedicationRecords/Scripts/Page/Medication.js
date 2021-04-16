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

        //On submit
        $("#frmMedicationForm").on("submit", function () {

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
                    Loading(false);
                    if (data.IsSuccess) {

                        $('#myModal').modal('show');
                        $("#modalContent").html('');
                        $("#modalContent").append('<label>Are you sure you want to update this record?</label>');
                        ModalAlert(MODAL_HEADER, "<label>Record successfully saved.</label>");
                        window.location.replace("/Medication");

                    } else {
                        if (data.IsListResult) {
                            var msg = "";

                            for (var i = 0; i < data.Result.length; i++) {
                                msg += "Error : " + data.Result[i] + "\n";
                            }

                            ModalAlert(MODAL_HEADER, msg);
                        }
                        else {
                            ModalAlert(MODAL_HEADER, "Error : " + data.Result);
                        }
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    Loading(false);
                    ModalAlert(MODAL_HEADER, "Error : " + $(jqXHR.responseText).filter('title').text());
                },
            });

            return false;
        });

    });
});