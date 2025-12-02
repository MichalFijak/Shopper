window.cardScrollIntoView = function(id) {
    var el = document.getElementById(id);
    if (el) {
        el.scrollIntoView({ behavior: "smooth", inline: "center", block: "nearest" });
    }
};

window._carouselObservers = window._carouselObservers || {};

window.observeCarousel = function (containerId, dotNetRef) {
    var container = document.getElementById(containerId);
    if (!container) return;

    if (window._carouselObservers[containerId]) {
        // already observing
        return;
    }

    var timeout = null;

    function onScrollEnd() {
        // find centered child
        var rect = container.getBoundingClientRect();
        var centerX = rect.left + rect.width / 2;
        var children = container.querySelectorAll('[id^="card-"]');
        var best = null;
        var bestDistance = Infinity;
        children.forEach(function (child) {
            var r = child.getBoundingClientRect();
            var childCenter = r.left + r.width / 2;
            var dist = Math.abs(childCenter - centerX);
            if (dist < bestDistance) {
                bestDistance = dist;
                best = child;
            }
        });

        if (best) {
            // ensure the best element is actually centered visually
            try {
                best.scrollIntoView({ behavior: 'smooth', inline: 'center', block: 'nearest' });
            } catch (e) {
                // ignore
            }

            // wait a moment for scroll to settle then notify Blazor
            setTimeout(function() {
                var value = best.getAttribute('data-value') || best.id.replace(/^card-/, '');
                try {
                    dotNetRef.invokeMethodAsync('NotifyCenteredCategory', value);
                } catch (e) {
                    console.error(e);
                }
            }, 220);
        }
    }

    function onScroll() {
        if (timeout) clearTimeout(timeout);
        // increase debounce to allow natural scrolling to finish
        timeout = setTimeout(onScrollEnd, 160);
    }

    container.addEventListener('scroll', onScroll, { passive: true });

    window._carouselObservers[containerId] = { onScroll: onScroll, timeout: timeout };

    // also trigger initial alignment check after a short delay
    timeout = setTimeout(onScrollEnd, 250);
};

window.disconnectCarousel = function (containerId) {
    var container = document.getElementById(containerId);
    if (!container) return;
    var obs = window._carouselObservers[containerId];
    if (!obs) return;
    container.removeEventListener('scroll', obs.onScroll);
    if (obs.timeout) clearTimeout(obs.timeout);
    delete window._carouselObservers[containerId];
};
