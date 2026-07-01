window.applySwipeTransform = function (id, x) {
    const el = document.getElementById(id);
    if (!el) return;

    el.style.transform = `translateX(${x}px)`;
    el.style.transition = "none";
    el.style.background = "rgba(0,255,180,0.08)";
};
window.applySwipeTransformRed = function (id, x) {
    const el = document.getElementById(id);
    if (!el) return;

    el.style.transform = `translateX(${x}px)`;
    el.style.transition = "none";
    el.style.background = "rgba(255, 80, 80, 0.08)";
};

window.resetSwipeTransform = function (id) {
    const el = document.getElementById(id);
    if (!el) return;

    el.style.transition = "transform 0.25s ease, background 0.25s ease";
    el.style.transform = "translateX(0)";
    el.style.background = "";
};

window.applySwipeRemove = function (id) {
    const el = document.getElementById(id);
    if (!el) return;

    el.style.transition = "transform 0.25s ease, opacity 0.25s ease";
    el.style.transform = "translateX(120px)";
    el.style.opacity = "0";

};
