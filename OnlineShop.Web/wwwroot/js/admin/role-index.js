let deleteId = null;

document.querySelectorAll(".delete-btn").forEach(btn => {
    btn.addEventListener("click", function () {
        deleteId = this.getAttribute("data-id");
        document.getElementById("deleteModal").classList.remove("hidden");
    });
});

document.getElementById("cancelDelete").addEventListener("click", function () {
    document.getElementById("deleteModal").classList.add("hidden");
});

document.getElementById("confirmDelete").addEventListener("click", function () {

    fetch(`/Admin/Roles/Delete/${deleteId}`, {
        method: "GET"
    })
        .then(r => r.text())
        .then(() => {
            document.getElementById("deleteModal").classList.add("hidden");

            const tr = document.querySelector(`tr[data-row="${deleteId}"]`);
            if (tr) {
                tr.classList.add("row-warning"); // فعال کردن خط قرمز
                setTimeout(() => {
                    tr.classList.add("row-collapse-bottom-up"); // فعال کردن انیمیشن جمع شدن از پایین
                }, 150);
                setTimeout(() => {
                    tr.remove(); // حذف نهایی بعد از اتمام انیمیشن
                }, 1200);
            }
        });
});