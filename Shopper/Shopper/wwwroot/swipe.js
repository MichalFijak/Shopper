window.applySwipeTransform = function (id, x) {
    const el = document.getElementById(id);
    if (!el) return;

    el.classList.add("swiping");
    el.style.transition = "none";
    el.style.transform = `translateX(${x}px)`;
    el.style.background = "rgba(0,255,180,0.08)";
};

window.applySwipeTransformRed = function (id, x) {
    const el = document.getElementById(id);
    if (!el) return;

    el.classList.add("swiping");
    el.style.transition = "none";
    el.style.transform = `translateX(${x}px)`;
    el.style.background = "rgba(255, 80, 80, 0.08)";
};

window.resetSwipeTransform = function (id) {
    const el = document.getElementById(id);
    if (!el) return;

    requestAnimationFrame(() => {
        requestAnimationFrame(() => {
            el.classList.remove("swiping");
            el.style.transition = "transform 0.25s ease, background 0.25s ease, opacity 0.25s ease";
            el.style.transform = "translateX(0)";
            el.style.background = "";
            el.style.opacity = "1";
        });
    });
};

window.applySwipeRemove = function (id) {
    const el = document.getElementById(id);
    if (!el) return;

    el.classList.remove("swiping");
    el.style.transition = "transform 0.25s ease, opacity 0.25s ease";
    el.style.transform = "translateX(120px)";
    el.style.opacity = "0";

    requestAnimationFrame(() => {
        requestAnimationFrame(() => {
            el.style.transition = "transform 0.25s ease, opacity 0.25s ease";
            el.style.transform = "translateX(0)";
            el.style.opacity = "1";
            el.style.background = "";
        });
    });
};
