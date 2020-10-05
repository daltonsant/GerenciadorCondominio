function ApproveUser(userId, name) {
    const url = 'users/ApproveUser';
    $.ajax({
        method: 'POST',
        url: url,
        data: { userId: userId },
        success: function (data) {
            if (data === true) {
                $("#" + userId).removeClass("purple darken-3").addClass("green darken-3").text("Approved");
                $("." + userId).children("a").remove();
                $("." + userId).append('<a class="btn-floating blue darken-4" href="Users/ManageUser?userId=' + userId + '&name=' + name + '" asp-controller="Users" asp-action="ManageUser" asp-route-userId="' + userId + '" asp-route-name="' + name + '"><i class="material-icons">group</i></a>');
                M.toast({
                    html: "User Approved",
                    classes: "green darken-3"
                });
            }
            else {
                M.toast({
                    html: "Wasn't possible to approve the user"
                });
            }
        }
    });
}

function ReproveUser(userId) {
    const url = 'users/ReproveUser';
    $.ajax({
        method: 'POST',
        url: url,
        data: { userId: userId },
        success: function (data) {
            if (data === true) {
                $("#" + userId).removeClass("purple darken-3").addClass("orange darken-3").text("Reproved");
                M.toast({
                    html: "User Reproved",
                    classes: "orange darken-3"
                });
            }
            else {
                M.toast({
                    html: "Wasn't possible to reprove the user"
                });
            }
        }
    });
}