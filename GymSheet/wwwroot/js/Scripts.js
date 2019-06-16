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

$(function () {
    $(".createObj").click(function () {
        $("#modal").load("/Objectives/Create", function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".editObj").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Objectives/Edit?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".deleteObj").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Objectives/Delete?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".createStudent").click(function () {
        $("#modal").load("/Students/Create", function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".editStudent").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Students/Edit?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".deleteStudent").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Students/Delete?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".createSheet").click(function () {
        var url = new URL(window.location);
        var StudentId = url.searchParams.get("StudentId");
        $("#modal").load("/Sheets/Create?StudentId=" + StudentId, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".editSheet").click(function () {
        var id = $(this).attr("data-id");
        var url = new URL(window.location);
        var StudentId = url.searchParams.get("StudentId");
        $("#modal").load("/Sheets/Edit?Id=" + id + "&StudentId=" + StudentId, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".deleteSheet").click(function () {
        var id = $(this).attr("data-id");
        var url = new URL(window.location);
        var StudentId = url.searchParams.get("StudentId");
        $("#modal").load("/Sheets/Delete?Id=" + id + "&StudentId=" + StudentId, function () {
            $("#modal").modal();
        })
    })
});

function k(i) {
    var v = i.value.replace(/\D/g, '');
    v = (v / 100).toFixed(2) + '';
    v = v.replace(".", ",");
    v = v.replace(/(\d)(\d{3})(\d{3}),/g, "$1.$2.$3,");
    v = v.replace(/(\d)(\d{3}),/g, "$1.$2,");
    i.value = v;
}

$(function () {
    $(".addExercise").click(function () {
        var id = $(this).attr("data-id");
        var url = new URL(window.location);
        var SheetId = url.searchParams.get("SheetId");
        var StudentId = url.searchParams.get("StudentId");
        $("#modal").load("/Sheets/AddExercise?ExerciseId=" + id + "&SheetId=" + SheetId + "&StudentId=" + StudentId, function () {
            $("#modal").modal();
        })
    })
});