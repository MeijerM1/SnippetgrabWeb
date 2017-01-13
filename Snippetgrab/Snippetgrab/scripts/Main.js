function seeTags(x) {
    if (document.getElementById("tags/" + x).hidden == false) {
        document.getElementById("tags/" + x).hidden = true;
    } else {
        document.getElementById("tags/" + x).hidden = false;
    }
}