// SVM Studio - Luxury Interactive JavaScript

$(document).ready(function() {
    // Initialize all luxury features
    initLuxuryCursor();
    initNavigationEffects();
    initScrollEffects();
    initGalleryLightbox();
    initParallaxEffects();
    initTextAnimations();
    initHeroCanvas();
    initMagneticButtons();
    initScrollToTop();
    initPageTransitions();
    initFormEnhancements();
    initCounterAnimations();
    initImageLazyLoading();
    initMiscellaneous();
    
    // Luxury Cursor System
    function initLuxuryCursor() {
        const cursor = $('.luxury-cursor');
        const cursorDot = $('.cursor-dot');
        const cursorOutline = $('.cursor-outline');
        
        if (cursor.length && !isMobile()) {
            $(document).mousemove(function(e) {
                const x = e.clientX;
                const y = e.clientY;
                
                cursorDot.css({
                    left: x + 'px',
                    top: y + 'px'
                });
                
                cursorOutline.css({
                    left: x + 'px',
                    top: y + 'px'
                });
            });
            
            // Hover effects
            $('a, button, .gallery-item, .luxury-btn').hover(
                function() {
                    cursor.addClass('hover');
                },
                function() {
                    cursor.removeClass('hover');
                }
            );
            
            // Click effects
            $(document).mousedown(function() {
                cursor.addClass('click');
            }).mouseup(function() {
                cursor.removeClass('click');
            });
        }
    }
    
    // Enhanced Navigation Effects
    function initNavigationEffects() {
        const navbar = $('#mainNavbar');
        const navBlur = $('.nav-bg-blur');
        
        $(window).scroll(function() {
            const scrollTop = $(this).scrollTop();
            
            if (scrollTop > 50) {
                navbar.addClass('scrolled');
                navBlur.addClass('active');
            } else {
                navbar.removeClass('scrolled');
                navBlur.removeClass('active');
            }
        });
        
        // Magnetic nav links
        $('.luxury-nav-link').each(function() {
            const link = $(this);
            const navLine = link.find('.nav-line');
            
            link.hover(
                function() {
                    gsap.to(navLine, {
                        duration: 0.3,
                        width: '100%',
                        ease: 'power2.out'
                    });
                },
                function() {
                    gsap.to(navLine, {
                        duration: 0.3,
                        width: '0%',
                        ease: 'power2.out'
                    });
                }
            );
        });
        
        // Mobile menu animations
        $('.luxury-toggler').click(function() {
            $(this).toggleClass('active');
            const lines = $(this).find('.toggler-line');
            
            if ($(this).hasClass('active')) {
                gsap.to(lines.eq(0), { rotation: 45, y: 8, duration: 0.3 });
                gsap.to(lines.eq(1), { opacity: 0, duration: 0.3 });
                gsap.to(lines.eq(2), { rotation: -45, y: -8, duration: 0.3 });
            } else {
                gsap.to(lines.eq(0), { rotation: 0, y: 0, duration: 0.3 });
                gsap.to(lines.eq(1), { opacity: 1, duration: 0.3 });
                gsap.to(lines.eq(2), { rotation: 0, y: 0, duration: 0.3 });
            }
        });
    }
    
    // Advanced Scroll Effects
    function initScrollEffects() {
        // Scroll progress bar
        const progressBar = $('.progress-bar');
        
        $(window).scroll(function() {
            const scrollTop = $(this).scrollTop();
            const docHeight = $(document).height() - $(window).height();
            const scrollPercent = (scrollTop / docHeight) * 100;
            
            progressBar.css('width', scrollPercent + '%');
        });
        
        // Parallax sections
        $('.luxury-hero-section').each(function() {
            gsap.to($(this).find('.hero-background'), {
                yPercent: -50,
                ease: 'none',
                scrollTrigger: {
                    trigger: this,
                    start: 'top bottom',
                    end: 'bottom top',
                    scrub: true
                }
            });
        });
        
        // Reveal animations
        $('.luxury-section').each(function() {
            gsap.from($(this).children(), {
                y: 100,
                opacity: 0,
                duration: 1,
                stagger: 0.1,
                ease: 'power2.out',
                scrollTrigger: {
                    trigger: this,
                    start: 'top 80%',
                    end: 'bottom 20%',
                    toggleActions: 'play none none reverse'
                }
            });
        });
    }
    
    // Enhanced Gallery Lightbox
    function initGalleryLightbox() {
        let currentImageIndex = 0;
        let galleryImages = [];
        
        // Build gallery array
        function buildGalleryArray() {
            galleryImages = [];
            $('[data-lightbox="gallery"]').each(function() {
                galleryImages.push({
                    src: $(this).data('src'),
                    title: $(this).closest('.luxury-gallery-item').find('.gallery-title').text(),
                    category: $(this).closest('.luxury-gallery-item').find('.gallery-category').text()
                });
            });
        }
        
        // Open lightbox
        function openLightbox(index) {
            currentImageIndex = index;
            const image = galleryImages[currentImageIndex];
            
            const lightboxHtml = `
                <div class="luxury-lightbox" id="luxuryLightbox">
                    <div class="lightbox-backdrop"></div>
                    <div class="lightbox-content">
                        <button class="lightbox-close" id="lightboxClose">
                            <i class="fas fa-times"></i>
                        </button>
                        <button class="lightbox-prev" id="lightboxPrev">
                            <i class="fas fa-chevron-left"></i>
                        </button>
                        <button class="lightbox-next" id="lightboxNext">
                            <i class="fas fa-chevron-right"></i>
                        </button>
                        <div class="lightbox-image-container">
                            <img src="${image.src}" alt="${image.title}" class="lightbox-image">
                        </div>
                        <div class="lightbox-info">
                            <h3 class="lightbox-title">${image.title}</h3>
                            <p class="lightbox-category">${image.category}</p>
                        </div>
                    </div>
                </div>
            `;
            
            $('body').append(lightboxHtml);
            $('body').addClass('lightbox-open');
            
            // Animate in
            gsap.fromTo('#luxuryLightbox', 
                { opacity: 0, scale: 0.8 },
                { opacity: 1, scale: 1, duration: 0.5, ease: 'power2.out' }
            );
            
            bindLightboxEvents();
        }
        
        // Close lightbox
        function closeLightbox() {
            gsap.to('#luxuryLightbox', {
                opacity: 0,
                scale: 0.8,
                duration: 0.3,
                ease: 'power2.in',
                onComplete: function() {
                    $('#luxuryLightbox').remove();
                    $('body').removeClass('lightbox-open');
                }
            });
        }
        
        // Bind lightbox events
        function bindLightboxEvents() {
            $('#lightboxClose, .lightbox-backdrop').click(closeLightbox);
            
            $('#lightboxPrev').click(function() {
                currentImageIndex = currentImageIndex > 0 ? currentImageIndex - 1 : galleryImages.length - 1;
                updateLightboxImage();
            });
            
            $('#lightboxNext').click(function() {
                currentImageIndex = currentImageIndex < galleryImages.length - 1 ? currentImageIndex + 1 : 0;
                updateLightboxImage();
            });
            
            $(document).keydown(function(e) {
                if (e.keyCode === 27) closeLightbox();
                if (e.keyCode === 37) $('#lightboxPrev').click();
                if (e.keyCode === 39) $('#lightboxNext').click();
            });
        }
        
        // Update lightbox image
        function updateLightboxImage() {
            const image = galleryImages[currentImageIndex];
            const lightboxImage = $('.lightbox-image');
            const lightboxTitle = $('.lightbox-title');
            const lightboxCategory = $('.lightbox-category');
            
            gsap.to(lightboxImage, {
                opacity: 0,
                duration: 0.2,
                onComplete: function() {
                    lightboxImage.attr('src', image.src).attr('alt', image.title);
                    lightboxTitle.text(image.title);
                    lightboxCategory.text(image.category);
                    gsap.to(lightboxImage, { opacity: 1, duration: 0.2 });
                }
            });
        }
        
        // Initialize gallery
        buildGalleryArray();
        
        $('[data-lightbox="gallery"]').click(function(e) {
            e.preventDefault();
            const index = $('[data-lightbox="gallery"]').index(this);
            openLightbox(index);
        });
    }
            $('.gallery-item').hide();
            $(`.gallery-item[data-category="${filter}"]`).fadeIn(400);
        }
    });
    
    // Parallax effect for hero section
    $(window).scroll(function() {
        const scrolled = $(this).scrollTop();
        const parallax = $('.hero-background');
        const speed = 0.5;
        parallax.css('transform', `translateY(${scrolled * speed}px)`);
    });
    
    // Form validation enhancement
    $('form').submit(function(e) {
        let isValid = true;
        const form = $(this);
        
        // Clear previous errors
        $('.is-invalid').removeClass('is-invalid');
        $('.invalid-feedback').remove();
        
        // Check required fields
        form.find('[required]').each(function() {
            const field = $(this);
            const value = field.val().trim();
            
            if (!value) {
                field.addClass('is-invalid');
                field.after('<div class="invalid-feedback">This field is required.</div>');
                isValid = false;
            }
        });
        
        // Email validation
        form.find('input[type="email"]').each(function() {
            const field = $(this);
            const email = field.val().trim();
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            
            if (email && !emailRegex.test(email)) {
                field.addClass('is-invalid');
                field.after('<div class="invalid-feedback">Please enter a valid email address.</div>');
                isValid = false;
            }
        });
        
        // Phone validation
        form.find('input[type="tel"]').each(function() {
            const field = $(this);
            const phone = field.val().trim();
            const phoneRegex = /^[\+]?[1-9][\d]{0,15}$/;
            
            if (phone && !phoneRegex.test(phone.replace(/[\s\-\(\)]/g, ''))) {
                field.addClass('is-invalid');
                field.after('<div class="invalid-feedback">Please enter a valid phone number.</div>');
                isValid = false;
            }
        });
        
        if (!isValid) {
            e.preventDefault();
            // Scroll to first error
            const firstError = $('.is-invalid').first();
            if (firstError.length) {
                $('html, body').animate({
                    scrollTop: firstError.offset().top - 100
                }, 500);
            }
        }
    });
    
    // Auto-hide alerts
    $('.alert').each(function() {
        const alert = $(this);
        setTimeout(function() {
            alert.fadeOut(400);
        }, 5000);
    });
    
    // Gallery lightbox functionality
    let currentImageIndex = 0;
    let galleryImages = [];
    
    function initGalleryLightbox() {
        galleryImages = $('.gallery-image').map(function() {
            return {
                src: $(this).attr('src'),
                title: $(this).siblings('.gallery-overlay').find('.gallery-title').text(),
                category: $(this).siblings('.gallery-overlay').find('.gallery-category').text()
            };
        }).get();
        
        $('.gallery-item').click(function() {
            currentImageIndex = $('.gallery-item').index(this);
            openLightbox();
        });
    }
    
    function openLightbox() {
        const image = galleryImages[currentImageIndex];
        const lightboxHtml = `
            <div class="lightbox-overlay" id="lightboxOverlay">
                <div class="lightbox-container">
                    <button class="lightbox-close" id="lightboxClose">&times;</button>
                    <button class="lightbox-prev" id="lightboxPrev">&#8249;</button>
                    <button class="lightbox-next" id="lightboxNext">&#8250;</button>
                    <img src="${image.src}" alt="${image.title}" class="lightbox-image">
                    <div class="lightbox-info">
                        <h3>${image.title}</h3>
                        <p>${image.category}</p>
                    </div>
                </div>
            </div>
        `;
        
        $('body').append(lightboxHtml);
        $('#lightboxOverlay').fadeIn(300);
        $('body').addClass('lightbox-open');
        
        // Lightbox controls
        $('#lightboxClose, #lightboxOverlay').click(function(e) {
            if (e.target === this) {
                closeLightbox();
            }
        });
        
        $('#lightboxPrev').click(function() {
            currentImageIndex = currentImageIndex > 0 ? currentImageIndex - 1 : galleryImages.length - 1;
            updateLightboxImage();
        });
        
        $('#lightboxNext').click(function() {
            currentImageIndex = currentImageIndex < galleryImages.length - 1 ? currentImageIndex + 1 : 0;
            updateLightboxImage();
        });
        
        // Keyboard navigation
        $(document).keydown(function(e) {
            if (e.keyCode === 27) closeLightbox(); // Escape
            if (e.keyCode === 37) $('#lightboxPrev').click(); // Left arrow
            if (e.keyCode === 39) $('#lightboxNext').click(); // Right arrow
        });
    }
    
    function updateLightboxImage() {
        const image = galleryImages[currentImageIndex];
        $('.lightbox-image').attr('src', image.src).attr('alt', image.title);
        $('.lightbox-info h3').text(image.title);
        $('.lightbox-info p').text(image.category);
    }
    
    function closeLightbox() {
        $('#lightboxOverlay').fadeOut(300, function() {
            $(this).remove();
        });
        $('body').removeClass('lightbox-open');
        $(document).off('keydown');
    }
    
    // Initialize lightbox if gallery exists
    if ($('.gallery-item').length > 0) {
        initGalleryLightbox();
    }
    
    // Animate counters
    function animateCounters() {
        $('.counter').each(function() {
            const counter = $(this);
            const target = parseInt(counter.data('target'));
            const duration = 2000;
            const increment = target / (duration / 16);
            let current = 0;
            
            const timer = setInterval(function() {
                current += increment;
                if (current >= target) {
                    current = target;
                    clearInterval(timer);
                }
                counter.text(Math.floor(current));
            }, 16);
        });
    }
    
    // Trigger counter animation when in view
    $(window).scroll(function() {
        $('.counter').each(function() {
            const counter = $(this);
            const elementTop = counter.offset().top;
            const elementBottom = elementTop + counter.outerHeight();
            const viewportTop = $(window).scrollTop();
            const viewportBottom = viewportTop + $(window).height();
            
            if (elementBottom > viewportTop && elementTop < viewportBottom && !counter.hasClass('animated')) {
                counter.addClass('animated');
                animateCounters();
            }
        });
    });
    
    // Loading overlay
    function showLoading() {
        const loadingHtml = `
            <div class="loading-overlay" id="loadingOverlay">
                <div class="spinner"></div>
            </div>
        `;
        $('body').append(loadingHtml);
    }
    
    function hideLoading() {
        $('#loadingOverlay').remove();
    }
    
    // AJAX form submission
    $('form[data-ajax="true"]').submit(function(e) {
        e.preventDefault();
        const form = $(this);
        const url = form.attr('action');
        const method = form.attr('method') || 'POST';
        const data = form.serialize();
        
        showLoading();
        
        $.ajax({
            url: url,
            type: method,
            data: data,
            success: function(response) {
                hideLoading();
                if (response.success) {
                    form[0].reset();
                    showAlert('success', response.message || 'Form submitted successfully!');
                } else {
                    showAlert('danger', response.message || 'An error occurred. Please try again.');
                }
            },
            error: function() {
                hideLoading();
                showAlert('danger', 'An error occurred. Please try again.');
            }
        });
    });
    
    // Show alert function
    function showAlert(type, message) {
        const alertHtml = `
            <div class="alert alert-${type} alert-dismissible fade show" role="alert">
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `;
        
        const alertContainer = $('.alert-container');
        if (alertContainer.length) {
            alertContainer.html(alertHtml);
        } else {
            $('main').prepend(`<div class="container alert-container">${alertHtml}</div>`);
        }
        
        // Auto-hide after 5 seconds
        setTimeout(function() {
            $('.alert').fadeOut(400);
        }, 5000);
    }
    
    // Intersection Observer for animations
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };
    
    const observer = new IntersectionObserver(function(entries) {
        entries.forEach(function(entry) {
            if (entry.isIntersecting) {
                entry.target.classList.add('active');
            }
        });
    }, observerOptions);
    
    // Observe elements for animation
    $('.fade-in, .slide-in-left, .slide-in-right').each(function() {
        observer.observe(this);
    });
    
    // Hero Canvas Animation
    function initHeroCanvas() {
        const canvas = document.getElementById('heroCanvas');
        if (!canvas) return;
        
        const ctx = canvas.getContext('2d');
        let animationId;
        
        function resizeCanvas() {
            canvas.width = window.innerWidth;
            canvas.height = window.innerHeight;
        }
        
        resizeCanvas();
        $(window).resize(resizeCanvas);
        
        // Particle system
        const particles = [];
        const particleCount = 50;
        
        for (let i = 0; i < particleCount; i++) {
            particles.push({
                x: Math.random() * canvas.width,
                y: Math.random() * canvas.height,
                vx: (Math.random() - 0.5) * 0.5,
                vy: (Math.random() - 0.5) * 0.5,
                radius: Math.random() * 2 + 1,
                opacity: Math.random() * 0.5 + 0.2
            });
        }
        
        function animate() {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            
            particles.forEach(particle => {
                particle.x += particle.vx;
                particle.y += particle.vy;
                
                if (particle.x < 0 || particle.x > canvas.width) particle.vx *= -1;
                if (particle.y < 0 || particle.y > canvas.height) particle.vy *= -1;
                
                ctx.beginPath();
                ctx.arc(particle.x, particle.y, particle.radius, 0, Math.PI * 2);
                ctx.fillStyle = `rgba(255, 255, 255, ${particle.opacity})`;
                ctx.fill();
            });
            
            animationId = requestAnimationFrame(animate);
        }
        
        animate();
        
        // Cleanup on page unload
        $(window).on('beforeunload', function() {
            cancelAnimationFrame(animationId);
        });
    }
    
    // Magnetic Buttons
    function initMagneticButtons() {
        $('.luxury-btn, .hero-cta, .gallery-btn').each(function() {
            const button = $(this);
            
            button.mousemove(function(e) {
                const buttonRect = this.getBoundingClientRect();
                const x = e.clientX - buttonRect.left - buttonRect.width / 2;
                const y = e.clientY - buttonRect.top - buttonRect.height / 2;
                
                gsap.to(this, {
                    x: x * 0.1,
                    y: y * 0.1,
                    duration: 0.3,
                    ease: 'power2.out'
                });
            });
            
            button.mouseleave(function() {
                gsap.to(this, {
                    x: 0,
                    y: 0,
                    duration: 0.5,
                    ease: 'elastic.out(1, 0.3)'
                });
            });
        });
    }
    
    // Enhanced Scroll to Top
    function initScrollToTop() {
        const scrollBtn = $('#scrollToTop');
        
        $(window).scroll(function() {
            const scrollTop = $(this).scrollTop();
            
            if (scrollTop > 300) {
                scrollBtn.addClass('show');
            } else {
                scrollBtn.removeClass('show');
            }
        });
        
        scrollBtn.click(function() {
            gsap.to(window, {
                scrollTo: 0,
                duration: 1,
                ease: 'power2.out'
            });
        });
    }
    
    // Page Transitions
    function initPageTransitions() {
        const transitionOverlay = $('.page-transition-overlay');
        
        $('a:not([href^="#"]):not([href^="mailto"]):not([href^="tel"]):not([target="_blank"])').click(function(e) {
            if ($(this).attr('href') && $(this).attr('href') !== '#') {
                e.preventDefault();
                const href = $(this).attr('href');
                
                gsap.to(transitionOverlay, {
                    x: '0%',
                    duration: 0.5,
                    ease: 'power2.inOut',
                    onComplete: function() {
                        window.location.href = href;
                    }
                });
            }
        });
        
        // Reset transition on page load
        gsap.set(transitionOverlay, { x: '100%' });
        gsap.to(transitionOverlay, {
            x: '100%',
            duration: 0.5,
            ease: 'power2.out',
            delay: 0.5
        });
    }
    
    // Enhanced Form Validation
    function initFormEnhancements() {
        $('form').each(function() {
            const form = $(this);
            const inputs = form.find('input, textarea, select');
            
            inputs.each(function() {
                const input = $(this);
                const wrapper = input.closest('.form-group, .mb-3');
                
                input.on('focus', function() {
                    wrapper.addClass('focused');
                });
                
                input.on('blur', function() {
                    wrapper.removeClass('focused');
                    if ($(this).val()) {
                        wrapper.addClass('filled');
                    } else {
                        wrapper.removeClass('filled');
                    }
                });
                
                // Check if already filled
                if (input.val()) {
                    wrapper.addClass('filled');
                }
            });
        });
        
        // AJAX form submission
        $('form[data-ajax="true"]').submit(function(e) {
            e.preventDefault();
            const form = $(this);
            const submitBtn = form.find('[type="submit"]');
            
            // Show loading
            submitBtn.addClass('loading').prop('disabled', true);
            
            $.ajax({
                url: form.attr('action'),
                type: form.attr('method') || 'POST',
                data: form.serialize(),
                success: function(response) {
                    showNotification('success', 'Form submitted successfully!');
                    form[0].reset();
                },
                error: function() {
                    showNotification('error', 'An error occurred. Please try again.');
                },
                complete: function() {
                    submitBtn.removeClass('loading').prop('disabled', false);
                }
            });
        });
    }
    
    // Counter Animations
    function initCounterAnimations() {
        $('.counter').each(function() {
            const counter = $(this);
            const target = parseInt(counter.data('target'));
            
            if (target) {
                gsap.to(counter, {
                    innerHTML: target,
                    duration: 2,
                    snap: { innerHTML: 1 },
                    ease: 'power2.out',
                    scrollTrigger: {
                        trigger: counter,
                        start: 'top 80%',
                        toggleActions: 'play none none reverse'
                    }
                });
            }
        });
    }
    
    // Parallax Effects
    function initParallaxEffects() {
        $('.parallax-element').each(function() {
            const element = $(this);
            const speed = element.data('speed') || 0.5;
            
            gsap.to(element, {
                yPercent: -50 * speed,
                ease: 'none',
                scrollTrigger: {
                    trigger: element,
                    start: 'top bottom',
                    end: 'bottom top',
                    scrub: true
                }
            });
        });
    }
    
    // Text Animations
    function initTextAnimations() {
        // Split text for character animation
        $('.hero-title .hero-line').each(function() {
            const text = $(this).text();
            const chars = text.split('').map(char => 
                char === ' ' ? '<span class="char">&nbsp;</span>' : `<span class="char">${char}</span>`
            );
            $(this).html(chars.join(''));
        });
        
        // Animate hero text
        gsap.from('.hero-title .char', {
            y: 100,
            opacity: 0,
            duration: 1,
            stagger: 0.02,
            ease: 'power2.out',
            delay: 0.5
        });
        
        // Animate section titles
        $('.section-title').each(function() {
            gsap.from(this, {
                y: 50,
                opacity: 0,
                duration: 1,
                ease: 'power2.out',
                scrollTrigger: {
                    trigger: this,
                    start: 'top 80%',
                    toggleActions: 'play none none reverse'
                }
            });
        });
    }
    
    // Image Lazy Loading
    function initImageLazyLoading() {
        const images = document.querySelectorAll('img[data-src]');
        
        if ('IntersectionObserver' in window) {
            const imageObserver = new IntersectionObserver((entries, observer) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        const img = entry.target;
                        img.src = img.dataset.src;
                        img.classList.remove('lazy');
                        observer.unobserve(img);
                    }
                });
            });
            
            images.forEach(img => imageObserver.observe(img));
        }
    }
    
    // Miscellaneous Functions
    function initMiscellaneous() {
        // Smooth scrolling for anchor links
        $('a[href^="#"]').on('click', function(event) {
            const target = $(this.getAttribute('href'));
            if (target.length) {
                event.preventDefault();
                gsap.to(window, {
                    scrollTo: target.offset().top - 100,
                    duration: 1,
                    ease: 'power2.out'
                });
            }
        });
        
        // Gallery filter functionality
        $('.filter-btn').click(function() {
            const filter = $(this).data('filter');
            $('.filter-btn').removeClass('active');
            $(this).addClass('active');
            
            if (filter === 'all' || filter === '*') {
                $('.gallery-item').fadeIn(400);
            } else {
                $('.gallery-item').hide();
                $(`.gallery-item[data-category="${filter}"]`).fadeIn(400);
            }
        });
        
        // Auto-hide alerts
        $('.alert').each(function() {
            const alert = $(this);
            setTimeout(function() {
                alert.fadeOut(400);
            }, 5000);
        });
    }
    
    // Utility Functions
    function isMobile() {
        return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
    }
    
    function showNotification(type, message) {
        const notification = $(`
            <div class="luxury-notification ${type}">
                <div class="notification-content">
                    <i class="fas fa-${type === 'success' ? 'check' : 'exclamation-triangle'}"></i>
                    <span>${message}</span>
                </div>
            </div>
        `);
        
        $('body').append(notification);
        
        gsap.from(notification, {
            x: 300,
            opacity: 0,
            duration: 0.5,
            ease: 'power2.out'
        });
        
        setTimeout(() => {
            gsap.to(notification, {
                x: 300,
                opacity: 0,
                duration: 0.5,
                ease: 'power2.in',
                onComplete: function() {
                    notification.remove();
                }
            });
        }, 4000);
    }
});
