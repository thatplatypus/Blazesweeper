export class GridVirtualize {
static observer;

static initIntersectionObserver(container) {
    observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                DotNet.invokeMethodAsync('Minesweeper.Client', 'OnItemVisible', entry.target.id);
            } else {
                DotNet.invokeMethodAsync('Minesweeper.Client', 'OnItemHidden', entry.target.id);
            }
        });
    }, {
        root: container,
        rootMargin: '0px',
        threshold: 0.1
    });
}

static observeItem(item) {
    observer.observe(item);
}
}

window.GridVirtualize = GridVirtualize;
window.initIntersectionObserver = GridVirtualize.initIntersectionObserver;