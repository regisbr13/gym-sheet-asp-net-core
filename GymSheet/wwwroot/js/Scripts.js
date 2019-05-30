$(function () {										
    $(".create").click(function () {
        $("#modal").load("MuscleGroups/Create", function () {
            $("#modal").modal();
        })
    })
});