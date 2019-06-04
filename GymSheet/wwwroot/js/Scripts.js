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

$(function () {
    $(".createExerc").click(function () {
        $("#modal").load("/Exercises/Create", function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".editExerc").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Exercises/Edit?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".deleteExerc").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Exercises/Delete?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});