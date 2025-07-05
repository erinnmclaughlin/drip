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