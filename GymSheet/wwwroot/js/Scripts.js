$(function () {										
    $(".create").click(function () {
        $("#modal").load("/MuscleGroups/Create", function () {
            $("#modal").modal();
        })
    })
});

$(function () {										
    $(".edit").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/MuscleGroups/Edit?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".delete").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/MuscleGroups/Delete?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});