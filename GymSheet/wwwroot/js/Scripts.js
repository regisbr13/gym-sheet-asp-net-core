function LoadImg(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        $(".img").show();
        reader.onload = function (e) {
            $(".img").attr('src', e.target.result).width(100).height(100);
        }
    }
    reader.readAsDataURL(input.files[0]);
}

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

$(function () {
    $(".createTeacher").click(function () {
        $("#modal").load("/Teachers/Create", function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".editTeacher").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Teachers/Edit?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".deleteTeacher").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Teachers/Delete?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});