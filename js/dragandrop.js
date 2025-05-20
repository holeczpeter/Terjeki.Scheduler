window.getDraggedDataFromEvent = () => {
    return window.lastDragData || "";
};

window.setDraggedData = (e, value) => {
    e.dataTransfer.setData("text/plain", value);
    window.lastDragData = value;
};