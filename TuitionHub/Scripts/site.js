// FAQ Toggle
document.addEventListener('DOMContentLoaded', function () {

    // ===== FAQ =====
    var faqQuestions = document.querySelectorAll('.faq-question');
    faqQuestions.forEach(function (q) {
        q.addEventListener('click', function () {
            var answer = this.nextElementSibling;
            var icon = this.querySelector('.icon');
            if (answer.classList.contains('open')) {
                answer.classList.remove('open');
                if (icon) icon.textContent = '+';
            } else {
                document.querySelectorAll('.faq-answer.open')
                    .forEach(function (a) { a.classList.remove('open'); });
                document.querySelectorAll('.faq-question .icon')
                    .forEach(function (i) { i.textContent = '+'; });
                answer.classList.add('open');
                if (icon) icon.textContent = '-';
            }
        });
    });

    // ===== FADE IN =====
    var fadeEls = document.querySelectorAll('.fade-in');
    if ('IntersectionObserver' in window) {
        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('visible');
                }
            });
        }, { threshold: 0.1 });
        fadeEls.forEach(function (el) { observer.observe(el); });
    } else {
        fadeEls.forEach(function (el) { el.classList.add('visible'); });
    }

});