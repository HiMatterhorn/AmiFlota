var routeURL = location.protocol + "//" + location.host;

$(document).ready(function () {
    $("#bookingStartDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false,
        format: "dd.MM.yyyy hh:mm"
    });

    $("#bookingEndDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false,
        format: "dd.MM.yyyy hh:mm"
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
                events: function (fetchInfo, successCallback, failureCallback) {
                    $.ajax({
                        url: routeURL + '/api/Booking/GetCalendarDataForCar?carVIN=' + $("#carVIN").val(),
                        type: 'GET',
                        dataType: 'JSON',
                        success: function (response) {
                            var events = [];
                            if (response.status === 1) {

                                $.each(response.dataenum, function (i, data) {
                                    events.push({
                                        title: data.Destination,
                                        description: data.UserName,
                                        start: data.StartDate,
                                        end: data.EndDate,
                                        backgroundColor: data.isApproved ? "#28a745" : "#dc3545",
                                        borderColor: "#162466",
                                        textColor: "white",
                                        id: data.Id
                                    });

                                })
                            }
                            successCallback(events);
                        },
                        error: function (xhr) {
                            $.notify("Error", "error");
                        }
                    });
                },
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