var routeURL = location.protocol + "//" + location.host;

$(document).ready(function () {
    $("#bookingStartDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false,
        format: "yyyy/MM/dd hh:mm"
    });

    $("#bookingEndDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false,
        format: "yyyy/MM/dd hh:mm"
    });

    InitializeCalendar();
});

var calendar;


function InitializeCalendar() {
    try {
        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',

                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                },
                eventDisplay: 'block',
                eventClick: function (info) {
                    getEventDetailsByEventId(info.event)
                }
            });
            calendar.render();
        }

    }
    catch (e) {
        alert(e);
    }
}

/*function onSearch() {
    if (checkValidation()) {


        var requestData = {
            Id: $("#bookingStartDate").val(),
            Title: $("#bookingEndDate").val(),
        };

        $.ajax({
            url: routeURL + '/api/Booking/GetAllAvailableCars',
            type: 'GET',
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (response) {
                if (response.status === 1 || response.status === 2) {

                    $.notify(response.message, "success");

                }
                else {
                    $.notify(response.message, "error");
                }
            },
            error: function (xhr) {
                $.notify("Error", "error");
            }
        });
    }
}*/

function checkValidation() {
    var isValid = true;

    /*   //TODO Validation rules
         if ($("#title").val() == undefined || $("#title").val() == "") {
            isValid = false;
        }
    
        if ($("#appointmentDate").val() == undefined || $("#appointmentDate").val() == "") {
            isValid = false;
    
        }*/

    return isValid;

}