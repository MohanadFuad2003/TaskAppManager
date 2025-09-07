document.querySelectorAll(".countdown").forEach(function (el) {
    const deadline = new Date(el.dataset.deadline);
    const timeTextEl = el.querySelector(".time-text");

    function updateCountdown() {
        const now = new Date();
        const diff = deadline - now;

        if (diff <= 0) {
            el.classList.remove("bg-secondary", "bg-warning", "bg-success");
            el.classList.add("bg-danger");
            timeTextEl.innerText = "Overdue";
            return;
        }

        const d = Math.floor(diff / (1000 * 60 * 60 * 24));
        const h = Math.floor((diff / (1000 * 60 * 60)) % 24);
        const m = Math.floor((diff / (1000 * 60)) % 60);
        const s = Math.floor((diff / 1000) % 60);

        if (diff < 86400000) {
            el.classList.remove("bg-secondary", "bg-success");
            el.classList.add("bg-warning");
        } else {
            el.classList.remove("bg-secondary", "bg-warning", "bg-danger");
            el.classList.add("bg-success");
        }

        timeTextEl.innerText = `${d}d ${h}h ${m}m ${s}s left`;
    }

    updateCountdown();
    setInterval(updateCountdown, 1000);
});
