window.toggleTheme = function () {
    const html = document.documentElement;
    const isLight = html.classList.toggle('light');
    if (isLight) {
        localStorage.setItem('theme', 'light');
    } else {
        localStorage.setItem('theme', 'dark');
    }
};

window.getCurrentTheme = function () {
    return document.documentElement.classList.contains('light') ? 'light' : 'dark';
};

// On load, set theme from localStorage
(function () {
    const theme = localStorage.getItem('theme');
    if (theme === 'light') {
        document.documentElement.classList.add('light');
    }
})();

// Document selector click outside handler
window.addDocumentSelectorClickOutsideHandler = function (element, dotNetHelper) {
    function handleClickOutside(event) {
        if (!element.contains(event.target)) {
            dotNetHelper.invokeMethodAsync('OnClickOutside');
        }
    }
    
    document.addEventListener('click', handleClickOutside);
    
    // Store the handler reference for cleanup
    element._clickOutsideHandler = handleClickOutside;
}; 