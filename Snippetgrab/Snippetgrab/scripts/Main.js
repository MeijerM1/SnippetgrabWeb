function seeTags(x) {
    if (document.getElementById("tags/" + x).hidden == false) {
        document.getElementById("tags/" + x).hidden = true;
    } else {
        document.getElementById("tags/" + x).hidden = false;
    }
}

function seeReplies(x) {
    if (document.getElementById("replies/" + x).hidden == false) {
        document.getElementById("replies/" + x).hidden = true;
    } else {
        document.getElementById("replies/" + x).hidden = false;
    }
}