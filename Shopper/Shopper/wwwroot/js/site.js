window.cardScrollIntoView = function(id) {
    var el = document.getElementById(id);
    if (el) {
        el.scrollIntoView({ behavior: "smooth", inline: "center", block: "nearest" });
    }
};
