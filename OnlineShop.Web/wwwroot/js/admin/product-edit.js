// ==============================
// CKEditor
// ==============================
CKEDITOR.replace('ShortDescription');
CKEDITOR.replace('Review');
CKEDITOR.replace('DetailReview');



// ==============================
// دسته بندی ها
// ==============================
window.onload = function () {
    fetch("/Admin/Categories/GetCategories")
        .then(res => res.json())
        .then(data => {
            var mainSelectList = document.getElementById("main-category");
            data.forEach(category => {
                var option = document.createElement('option');
                option.value = category.id;
                option.textContent = category.title;
                mainSelectList.appendChild(option);
            });
        });
}
function loadSubCategory(parentId) {
    fetch(`/Admin/Categories/GetCategories?parentId=${parentId}`)
        .then(res => res.json())
        .then(data => {
            var subSelectList = document.getElementById("sub-category");
            subSelectList.innerHTML = '<option value="">انتخاب دسته بندی</option>';
            data.forEach(category => {
                var option = document.createElement('option');
                option.value = category.id;
                option.textContent = category.title;
                subSelectList.appendChild(option);
            });
        });

}
function loadFinalCategory(parentId) {
    fetch(`/Admin/Categories/GetCategories?parentId=${parentId}`)
        .then(res => res.json())
        .then(data => {
            var finalSelectList = document.getElementById("final-category");
            finalSelectList.innerHTML = '<option value="">انتخاب دسته بندی</option>';
            data.forEach(category => {
                var option = document.createElement('option');
                option.value = category.id;
                option.textContent = category.title;
                finalSelectList.appendChild(option);
            });
        });
}


// ==============================
// Tagify
// ==============================
const tagInput = document.querySelector("#TagifyCustomInlineSuggestion");

const tagify = new Tagify(tagInput, {
    maxTags: 20,
    dropdown: {
        classname: "tags-inline",
        maxItems: 20,
        enabled: 1,       // بعد از ۱ کاراکتر جستجو کن
        closeOnSelect: false
    }
});

// debounce ساده برای جلوگیری از رگبار درخواست‌ها
function debounce(fn, delay) {
    let timer;
    return (...args) => {
        clearTimeout(timer);
        timer = setTimeout(() => fn(...args), delay);
    };
}

const fetchTags = debounce(function (value) {
    tagify.loading(true);

    fetch(`/Admin/Products/GetTags?query=${encodeURIComponent(value)}`)
        .then(res => {
            if (!res.ok) throw new Error("Error loading tags");
            return res.json();
        })
        .then(tags => {
            tagify.whitelist = tags;
            tagify.loading(false).dropdown.show(value);
        })
        .catch(() => tagify.loading(false));
}, 300);

// هر بار کاربر تایپ می‌کنه
tagify.on("input", function (e) {
    const value = e.detail.value.trim();
    if (value.length < 1) return;
    fetchTags(value);
});


// ==============================
// حذف تصاویر گالری
// ==============================
document.querySelectorAll(".remove-gallery")
    .forEach(btn => {

        btn.addEventListener("click", function () {

            let id = this.dataset.id;

            // مخفی شدن عکس

            document
                .getElementById("gallery-" + id)
                .remove();

            // ساخت hidden

            let input = document.createElement("input");

            input.type = "hidden";
            input.name = "DeletedGalleryIds";
            input.value = id;

            document
                .getElementById("deleted-gallery-container")
                .appendChild(input);

        });

    });

// ==============================
// پیش نمایش تصاویر گالری
// ==============================
document.querySelector("input[name='Galleries']")
    .addEventListener("change", function () {
        let previewContainer = document.getElementById("gallery-preview");
        previewContainer.innerHTML = "";
        Array.from(this.files).forEach(file => {
            let reader = new FileReader();
            reader.onload = function (e) {
                let img = document.createElement("img");
                img.src = e.target.result;
                img.className = "img-thumbnail m-1";
                img.style.maxWidth = "150px";
                previewContainer.appendChild(img);
            }
            reader.readAsDataURL(file);
        });
    });

// ==============================
// تصویر اصلی محصول
// ==============================
function ShowpImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgProduct').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}