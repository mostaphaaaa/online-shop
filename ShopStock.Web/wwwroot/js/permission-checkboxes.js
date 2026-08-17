// permission-checkboxes.js
document.addEventListener('DOMContentLoaded', function () {
    // انتخاب همه چک‌باکس‌های موجود در صفحه
    const allCheckboxes = document.querySelectorAll('input.form-check-input[type="checkbox"]');

    // اگر چک‌باکسی وجود نداشت، از تابع خارج شو
    if (allCheckboxes.length === 0) return;

    // تابع برای پیدا کردن همه فرزندان مستقیم یک والد مشخص
    function getDirectChildren(parentCol) {
        const directCollapse = parentCol.querySelector(':scope > .collapse');
        if (!directCollapse) return [];

        const directChildCols = directCollapse.querySelectorAll(':scope > .col-md');
        const checkboxes = [];
        directChildCols.forEach(col => {
            const checkbox = col.querySelector(':scope > .form-check > .form-check-label > input.form-check-input, :scope > div > .form-check > .form-check-label > input.form-check-input');
            if (checkbox) {
                checkboxes.push(checkbox);
            }
        });
        return checkboxes;
    }

    // تابع برای پیدا کردن والد یک چک‌باکس
    function getParentColumn(checkbox) {
        const currentCol = checkbox.closest('.col-md');
        if (!currentCol) return null;

        const parentCollapse = currentCol.closest('.collapse');
        if (!parentCollapse) return null;

        const parentCol = parentCollapse.closest('.col-md');
        return parentCol || null;
    }

    // تابع برای پیدا کردن چک‌باکس والد
    function getParentCheckbox(checkbox) {
        const parentCol = getParentColumn(checkbox);
        if (!parentCol) return null;

        const parentCheckbox = parentCol.querySelector('input.form-check-input[type="checkbox"]');
        return parentCheckbox;
    }

    // فقط تیک زدن والد (هیچوقت تیک والد را بر نمی‌داریم)
    function checkParentOnly(parentCol) {
        if (!parentCol) return;

        const parentCheckbox = parentCol.querySelector('input.form-check-input[type="checkbox"]');
        if (!parentCheckbox) return;

        const directChildren = getDirectChildren(parentCol);
        if (directChildren.length === 0) return;

        const atLeastOneChecked = Array.from(directChildren).some(child => child.checked);
        if (atLeastOneChecked) {
            parentCheckbox.checked = true;
        }
    }

    // به‌روزرسانی تمام والدین به صورت بازگشتی (فقط برای تیک زدن)
    function updateAllParents(checkbox) {
        let currentCheckbox = checkbox;
        let parentCheckbox = getParentCheckbox(currentCheckbox);

        while (parentCheckbox) {
            const parentCol = getParentColumn(currentCheckbox);
            if (parentCol) {
                checkParentOnly(parentCol);
            }
            currentCheckbox = parentCheckbox;
            parentCheckbox = getParentCheckbox(currentCheckbox);
        }
    }

    // اضافه کردن event listener به همه چک‌باکس‌ها
    allCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function () {
            if (!this.checked) {
                uncheckAllChildren(this);
            }

            if (this.checked) {
                updateAllParents(this);
            }
        });
    });

    // تابع برای برداشتن تیک همه فرزندان
    function uncheckAllChildren(checkbox) {
        const checkboxCol = checkbox.closest('.col-md');
        if (!checkboxCol) return;

        const collapse = checkboxCol.querySelector(':scope > .collapse');
        if (!collapse) return;

        const childCheckboxes = collapse.querySelectorAll('input.form-check-input[type="checkbox"]');
        childCheckboxes.forEach(child => {
            child.checked = false;
        });
    }

    // اجرای اولیه برای هماهنگ شدن وضعیت والدها با فرزندان هنگام بارگذاری صفحه
    function initializeParents() {
        const allCols = document.querySelectorAll('.col-md');
        allCols.forEach(col => {
            const hasCollapse = col.querySelector(':scope > .collapse');
            if (hasCollapse) {
                const parentCheckbox = col.querySelector('input.form-check-input[type="checkbox"]');
                if (parentCheckbox && !parentCheckbox.checked) {
                    checkParentOnly(col);
                }
            }
        });
    }

    initializeParents();
});