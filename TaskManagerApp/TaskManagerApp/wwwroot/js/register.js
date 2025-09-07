const form = document.querySelector("form");
const btn = document.getElementById("registerBtn");
const btnText = document.getElementById("registerBtnText");
const spinner = document.getElementById("registerBtnSpinner");

if (form && btn && btnText && spinner) {
    form.addEventListener("submit", () => {
        btn.disabled = true;
        btnText.textContent = "Processing...";
        spinner.classList.remove("d-none");
    });
}
