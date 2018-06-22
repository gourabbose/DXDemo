
function AddPersonViewModel() {
    this.PersonType = ko.observable("Producer");
    this.Name = ko.observable("");
    this.Sex = ko.observable("");
    this.DOB = ko.observable("");
    this.Bio = ko.observable("");
    this.Header = ko.computed(function () {
        return "Add " + this.PersonType();
    }, this);
    this.ValidateAndSubmit = function () {
        var isValid = true;
        if (this.Name() === "") {
            isValid = false;
            $("#errName").text("Please enter a Name!");
        }
        else $("#errName").text("");
        if (this.DOB() === "") {
            isValid = false;
            $("#errDOB").text("Please enter DOB!");
        } else $("#errDOB").text("");
        if (this.Bio() === "") {
            isValid = false;
            $("#errBio").text("Please enter a Bio!");
        } else $("#errBio").text("");
        if (isValid) {
            var url = '';
            if (this.PersonType() === "Producer")
                url = '/Person/AddProducer';
            else
                url = '/Person/AddActor';
            $.ajax(url, {
                data: ko.toJSON(viewModel),
                type: "post", contentType: "application/json",
                success: function (result) {
                    if (result.status === true) {
                        $(dialog).dialog('close');
                        var url = viewModel.PersonType() === "Producer" ? "/Person/GetProducers" : "/Person/GetActors";
                        $.get(url, function (result) {
                            if (result.status === true) {
                                var list = viewModel.PersonType() === "Producer" ? $('#ProducerId') : $('#Actors');
                                list.html('');
                                if (viewModel.PersonType() === "Producer") list.append('<option value="-1">-</option>');
                                $.each(result.data, function (key, value) {
                                    list.append('<option value="' + value.id + '">' + value.name + '</option>');
                                });
                            }
                            else {
                                alert(viewModel.PersonType() +  " list refresh failed. Please reload the page.");
                            }
                            viewModel.ClearModel();
                        });
                    }
                    else {
                        $(dialog).dialog('close');
                        alert(viewModel.PersonType() + " addition failed!");
                        viewModel.ClearModel();
                    }
                }
            });
        }
    };
    this.ClearModel = function () {
        this.PersonType('');
        this.Name("");
        this.Sex("");
        this.DOB("");
        this.Bio("");
    };
}
var viewModel = new AddPersonViewModel();
ko.applyBindings(viewModel);

$(document).ready(function () {
    $("#btnSubmit").click(function (e) {
        if ($("#Actors").find(":selected").length < 1) {
            $("#actorError").text("Please select actors!");
            e.preventDefault();
        }
        else {
            $("#actorError").text("");
        }
        if (operation === undefined) {
            if ($('input#poster')[0].files.length < 1) {
                $("#posterError").text("Please select a poster!");
                e.preventDefault();
            }
            else {
                $("#posterError").text("");
            }
        }
    });
    $('#poster').change(function () {
        var ext = this.value.match(/\.(.+)$/)[1];
        switch (ext) {
            case 'jpg':
            case 'jpeg':
            case 'png':
            case 'gif':
                break;
            default:
                alert('This is not an allowed file type.');
                this.value = '';
        }
    });
});
var dialog = $("#newPerson").dialog({
    autoOpen: false,
    width: 600,
    height: 500
});
$("#datepicker").datepicker();
$('#lnkAddActor').click(function () {
    viewModel.PersonType('Actor');
    $(dialog).dialog('open');
});
$('#lnkAddProducer').click(function () {
    viewModel.PersonType('Producer');
    $(dialog).dialog('open');
});