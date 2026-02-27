window.coronaDocsLayout = (() => {
    let resizeHandler;

    return {
        initialize(dotNetRef) {
            resizeHandler = () => dotNetRef.invokeMethodAsync('OnBrowserResized', window.innerWidth);
            window.addEventListener('resize', resizeHandler);
            return window.innerWidth;
        },
        dispose() {
            if (resizeHandler) {
                window.removeEventListener('resize', resizeHandler);
                resizeHandler = null;
            }
        }
    };
})();
