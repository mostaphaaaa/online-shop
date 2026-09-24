const colorCodeInput = document.getElementById("colorCode");
const colorPreview = document.getElementById("colorPreview");

function updateColorPreview() {
    const color = colorCodeInput.value.trim();

    if (!color) {
        colorPreview.style.backgroundColor = "#ffffff";
        return;
    }

    const testElement = new Option();

    try {
        testElement.style.color = color;

        if (testElement.style.color) {
            colorPreview.style.backgroundColor = color;
        }
    } catch {
        // کد رنگ نامعتبر است؛ پیش‌نمایش تغییر نمی‌کند.
    }
}

colorCodeInput.addEventListener("input", updateColorPreview);

updateColorPreview();