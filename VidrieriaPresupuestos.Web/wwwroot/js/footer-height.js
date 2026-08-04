window.observarAlturaFooter = (footerElement) => {
    const actualizarAltura = () => {
        document.documentElement.style.setProperty('--app-footer-height', `${footerElement.offsetHeight}px`);
    };

    new ResizeObserver(actualizarAltura).observe(footerElement);
    actualizarAltura();
};
