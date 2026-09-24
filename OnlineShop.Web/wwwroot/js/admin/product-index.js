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

    const token = document.querySelector(
        '#deleteForm input[name="__RequestVerificationToken"]'
    ).value;

    fetch("/Admin/Products/DeleteConfirmed", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
            "RequestVerificationToken": token
        },
        body: `id=${deleteId}`
    })
        .then(r => {
            if (!r.ok) {
                throw new Error(`HTTP ${r.status}`);
            }

            return r.text();
        })
        .then(() => {
            document.getElementById("deleteModal").classList.add("hidden");

            const tr = document.querySelector(`tr[data-row="${deleteId}"]`);

            if (tr) {
                tr.classList.add("row-warning");

                setTimeout(() => {
                    tr.classList.add("row-collapse-bottom-up");
                }, 150);

                setTimeout(() => {
                    tr.remove();
                }, 1200);
            }
        })
        .catch(error => {
            console.error(error);
        });
});
