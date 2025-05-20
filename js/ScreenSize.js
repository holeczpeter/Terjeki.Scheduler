window.getScreenWidth = () => window.innerWidth;
let timeout;

function throttle(func, limit) {
    return function () {
        const context = this;
        const args = arguments;

        if (!timeout) {
            timeout = setTimeout(() => {
                func.apply(context, args);
                timeout = null;
            }, limit);
        }
    };
}

window.onScreenResize = (dotNetHelper) => {
    const handleResize = throttle(() => {
        dotNetHelper.invokeMethodAsync('SetLayoutBasedOnScreenSize');
    }, 200);

    window.addEventListener('resize', handleResize);
};