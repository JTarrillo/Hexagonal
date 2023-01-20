function fnOnChangeOnClick(cbocatsChildByParentIdLeveledit0, cbocatsChildByParentIdLeveledit1, cbocatParentsedit) {

    $(cbocatsChildByParentIdLeveledit0).on("change", function () {
        if ($(cbocatsChildByParentIdLeveledit0).val() != 6) {
            $(cbocatsChildByParentIdLeveledit1).css("display", "none");
        }
        else {
            $(cbocatsChildByParentIdLeveledit1).css("display", "table");
        }
    });

    $(cbocatParentsedit).on("change", function () {
        if ($(cbocatParentsedit).val() != 1) {
            $(cbocatsChildByParentIdLeveledit0).css("display", "none");
            $(cbocatsChildByParentIdLeveledit1).css("display", "none");
        }
        else {
            $(cbocatsChildByParentIdLeveledit0).css("display", "table");
            $(cbocatsChildByParentIdLeveledit1).css("display", "table");
        }
    });

    $(cbocatParentsedit).on("click", function () {
        $(cbocatsChildByParentIdLeveledit0).val(6);
        $(cbocatsChildByParentIdLeveledit1).val(8);
    });

    $(cbocatsChildByParentIdLeveledit0).on("click", function () {
        $(cbocatsChildByParentIdLeveledit1).val(8);
    });
}

function fnLoadInputSelectsEdit(
    id_parent,
    id_parent2,
    id_parent3,
    cboCatParentsEdit,
    cboCatsChildByParentIdLevelEdit0,
    cboCatsChildByParentIdLevelEdit1
) {
    if (id_parent != -1 && id_parent2 != -1 && id_parent3 != 3) {
        setTimeout(function () {
            $(cboCatParentsEdit).val(id_parent3);
            $(cboCatsChildByParentIdLevelEdit0).val(id_parent2);
            $(cboCatsChildByParentIdLevelEdit1).val(id_parent);
            $("#loading").removeClass("flex");
        }, 1000);
    }

    if (id_parent != -1 && id_parent2 == -1 && id_parent3 == -1) {
        setTimeout(function () {
            $(cboCatParentsEdit).val(id_parent);
            if ($(cboCatsChildByParentIdLevelEdit0).val() == null && $(cboCatsChildByParentIdLevelEdit1).val() == null) {
                $(cboCatsChildByParentIdLevelEdit0).css("display", "none");
                $(cboCatsChildByParentIdLevelEdit1).css("display", "none");
                $("#loading").removeClass("flex");
            }
        }, 1000);
    }

    if (id_parent != -1 && id_parent2 != -1 && id_parent3 == -1) {
        setTimeout(function () {
            $(cboCatParentsEdit).val(id_parent2);
            $(cboCatsChildByParentIdLevelEdit0).val(id_parent);

            if ($(cboCatsChildByParentIdLevelEdit1).val() == null) {
                $(cboCatsChildByParentIdLevelEdit1).css("display", "none");
            }
            $("#loading").removeClass("flex");
        }, 1000);
    }
}