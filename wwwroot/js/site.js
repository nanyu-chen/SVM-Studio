// SVM Studio - Dreamy Editorial JavaScript

$(document).ready(function() {
    // Initialize all features
    initSmoothScrolling();
    initGalleryLightbox();
    initScrollToTop();
    initFormEnhancements();
    initNavigationEffects();
    initCounterAnimations();
    initImageLazyLoading();
    initParallaxEffects();
    initGalleryFilters();
    initAnimationOnScroll();
    initHeroCarousel();
    
    // Hero carousel initialization
    function initHeroCarousel() {
        // Auto-play carousel with fade effect
        $('.hero-carousel').on('slide.bs.carousel', function(e) {
            var $animatingElems = $(e.relatedTarget).find('.fade-in');
            $animatingElems.each(function() {
                var $this = $(this);
                var animationDelay = $this.data('delay') || 0;
                setTimeout(function() {
                    $this.addClass('active');
                }, animationDelay);
            });
        });
    }
    
    // Smooth scrolling for anchor links
    function initSmoothScrolling() {
        $('a[href^="#"]').on('click', function(e) {
            e.preventDefault();
            
            var target = $(this.getAttribute('href'));
            if(target.length) {
                $('html, body').animate({
                    scrollTop: target.offset().top - 100
                }, 1000, 'easeInOutCubic');
            }
        });
    }

    // Enhanced gallery lightbox functionality
    function initGalleryLightbox() {
        $('.gallery-item').on('click', function(e) {
            e.preventDefault();
            
            var imgSrc = $(this).find('img').attr('src');
            var imgAlt = $(this).find('img').attr('alt');
            var title = $(this).find('h4').text() || '';
            var description = $(this).find('p').text() || '';
            
            var lightboxHTML = `
                <div class="lightbox-overlay">
                    <div class="lightbox-content">
                        <button class="lightbox-close" aria-label="Close">&times;</button>
                        <img src="${imgSrc}" alt="${imgAlt}">
                        ${title ? `<div class="lightbox-info"><h4>${title}</h4><p>${description}</p></div>` : ''}
                    </div>
                </div>
            `;
            
            $('body').append(lightboxHTML);
            $('.lightbox-overlay').hide().fadeIn(300);
            
            // Close lightbox functionality
            $('.lightbox-overlay').on('click', function(e) {
                if(e.target === this || $(e.target).hasClass('lightbox-close')) {
                    $(this).fadeOut(300, function() {
                        $(this).remove();
                    });
                }
            });
            
            // Close lightbox on ESC key
            $(document).on('keydown.lightbox', function(e) {
                if(e.key === 'Escape') {
                    $('.lightbox-overlay').fadeOut(300, function() {
                        $(this).remove();
                    });
                    $(document).off('keydown.lightbox');
                }
            });
        });
    }

    // Enhanced scroll to top button
    function initScrollToTop() {
        var scrollToTop = $('.scroll-to-top');
        
        if(scrollToTop.length === 0) {
            $('body').append('<button class="scroll-to-top" id="scrollToTop"><i class="fas fa-arrow-up"></i></button>');
            scrollToTop = $('#scrollToTop');
        }
        
        // Show/hide scroll to top button with smooth animation
        $(window).scroll(function() {
            if($(this).scrollTop() > 300) {
                scrollToTop.addClass('show');
            } else {
                scrollToTop.removeClass('show');
            }
        });
        
        // Smooth scroll to top
        scrollToTop.on('click', function() {
            $('html, body').animate({
                scrollTop: 0
            }, 1000, 'easeInOutCubic');
        });
    }

    // Enhanced form functionality
    function initFormEnhancements() {
        // Floating labels effect
        $('.form-floating input, .form-floating textarea').on('focus blur', function() {
            $(this).parent().toggleClass('focused', $(this).is(':focus') || $(this).val().length > 0);
        });
        
        // Form validation with enhanced feedback
        $('form').on('submit', function(e) {
            var isValid = true;
            var form = $(this);
            
            // Clear previous errors
            form.find('.is-invalid').removeClass('is-invalid');
            form.find('.error-message').remove();
            
            // Check required fields
            form.find('[required]').each(function() {
                var field = $(this);
                if(!field.val().trim()) {
                    isValid = false;
                    field.addClass('is-invalid');
                    field.after('<div class="error-message text-danger">This field is required</div>');
                }
            });
            
            // Check email format
            form.find('input[type="email"]').each(function() {
                var field = $(this);
                var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if(field.val() && !emailPattern.test(field.val())) {
                    isValid = false;
                    field.addClass('is-invalid');
                    field.after('<div class="error-message text-danger">Please enter a valid email address</div>');
                }
            });
            
            if(!isValid) {
                e.preventDefault();
                showNotification('error', 'Please correct the errors above.');
                // Scroll to first error
                var firstError = form.find('.is-invalid').first();
                if(firstError.length) {
                    $('html, body').animate({
                        scrollTop: firstError.offset().top - 100
                    }, 500);
                }
            }
        });
    }

    // Enhanced navigation effects
    function initNavigationEffects() {
        var navbar = $('.navbar');
        
        // Add/remove background on scroll with smooth transition
        $(window).scroll(function() {
            if($(this).scrollTop() > 50) {
                navbar.addClass('scrolled');
            } else {
                navbar.removeClass('scrolled');
            }
        });
        
        // Close mobile menu when clicking outside
        $(document).on('click', function(e) {
            if(!$(e.target).closest('.navbar').length) {
                $('.navbar-collapse').collapse('hide');
            }
        });
        
        // Smooth page transitions
        $('a[href^="/"]:not([href^="/#"])').on('click', function(e) {
            var href = $(this).attr('href');
            if(href && href !== window.location.pathname) {
                e.preventDefault();
                $('body').fadeOut(300, function() {
                    window.location.href = href;
                });
            }
        });
    }

    // Enhanced counter animations
    function initCounterAnimations() {
        $('.counter').each(function() {
            var $this = $(this);
            var countTo = $this.attr('data-count');
            var countFrom = $this.attr('data-from') || 0;
            
            if(countTo) {
                var observer = new IntersectionObserver(function(entries) {
                    entries.forEach(function(entry) {
                        if(entry.isIntersecting) {
                            $({ countNum: countFrom }).animate({
                                countNum: countTo
                            }, {
                                duration: 3000,
                                easing: 'swing',
                                step: function() {
                                    $this.text(Math.floor(this.countNum));
                                },
                                complete: function() {
                                    $this.text(countTo);
                                }
                            });
                            observer.unobserve(entry.target);
                        }
                    });
                });
                
                observer.observe(this);
            }
        });
    }

    // Enhanced image lazy loading
    function initImageLazyLoading() {
        if('IntersectionObserver' in window) {
            var imageObserver = new IntersectionObserver(function(entries) {
                entries.forEach(function(entry) {
                    if(entry.isIntersecting) {
                        var img = entry.target;
                        var src = img.dataset.src;
                        
                        if(src) {
                            img.src = src;
                            img.classList.remove('lazy');
                            img.classList.add('fade-in');
                        }
                        
                        imageObserver.unobserve(img);
                    }
                });
            });
            
            document.querySelectorAll('img[data-src]').forEach(function(img) {
                imageObserver.observe(img);
            });
        }
    }

    // Parallax effects
    function initParallaxEffects() {
        var parallaxElements = $('.parallax-element');
        
        if(parallaxElements.length > 0) {
            $(window).scroll(function() {
                var scrolled = $(this).scrollTop();
                
                parallaxElements.each(function() {
                    var $element = $(this);
                    var speed = $element.data('speed') || 0.5;
                    var yPos = -(scrolled * speed);
                    $element.css('transform', 'translateY(' + yPos + 'px)');
                });
            });
        }
    }

    // Gallery filters
    function initGalleryFilters() {
        $('.filter-btn').on('click', function() {
            var filter = $(this).data('filter');
            var galleryItems = $('.gallery-item');
            
            // Update active button
            $('.filter-btn').removeClass('active');
            $(this).addClass('active');
            
            // Filter items
            if(filter === 'all') {
                galleryItems.fadeIn(300);
            } else {
                galleryItems.each(function() {
                    var categories = $(this).data('categories') || '';
                    if(categories.includes(filter)) {
                        $(this).fadeIn(300);
                    } else {
                        $(this).fadeOut(300);
                    }
                });
            }
        });
    }

    // Animation on scroll
    function initAnimationOnScroll() {
        var animatedElements = $('.fade-in, .slide-in-left, .slide-in-right');
        
        if(animatedElements.length > 0 && 'IntersectionObserver' in window) {
            var animationObserver = new IntersectionObserver(function(entries) {
                entries.forEach(function(entry) {
                    if(entry.isIntersecting) {
                        var element = entry.target;
                        var delay = element.dataset.delay || 0;
                        
                        setTimeout(function() {
                            element.classList.add('active');
                        }, delay);
                        
                        animationObserver.unobserve(element);
                    }
                });
            }, {
                threshold: 0.1
            });
            
            animatedElements.each(function() {
                animationObserver.observe(this);
            });
        }
    }

    // Enhanced notification system
    function showNotification(type, message, duration = 5000) {
        var notification = $(`
            <div class="notification notification-${type}">
                <div class="notification-content">
                    <i class="fas fa-${type === 'success' ? 'check-circle' : type === 'error' ? 'exclamation-triangle' : 'info-circle'}"></i>
                    <span>${message}</span>
                    <button class="notification-close">&times;</button>
                </div>
            </div>
        `);
        
        // Add notification to container
        if($('.notification-container').length === 0) {
            $('body').append('<div class="notification-container"></div>');
        }
        
        $('.notification-container').append(notification);
        
        // Animate in
        notification.hide().slideDown(300);
        
        // Close functionality
        notification.find('.notification-close').on('click', function() {
            notification.slideUp(300, function() {
                $(this).remove();
            });
        });
        
        // Auto-hide
        setTimeout(function() {
            notification.slideUp(300, function() {
                $(this).remove();
            });
        }, duration);
    }

    // Contact form enhancements
    function initContactForm() {
        $('#contactForm').on('submit', function(e) {
            e.preventDefault();
            
            var formData = $(this).serialize();
            var submitBtn = $(this).find('button[type="submit"]');
            var originalText = submitBtn.text();
            
            // Show loading state
            submitBtn.text('Sending...').prop('disabled', true);
            
            // Simulate form submission (replace with actual endpoint)
            setTimeout(function() {
                showNotification('success', 'Thank you for your message! We\'ll get back to you soon.');
                $('#contactForm')[0].reset();
                submitBtn.text(originalText).prop('disabled', false);
            }, 2000);
        });
    }

    // Initialize contact form if it exists
    if($('#contactForm').length > 0) {
        initContactForm();
    }

    // Custom easing functions
    $.easing.easeInOutCubic = function(x, t, b, c, d) {
        if ((t/=d/2) < 1) return c/2*t*t*t + b;
        return c/2*((t-=2)*t*t + 2) + b;
    };

    // Preloader functionality
    function initPreloader() {
        $(window).on('load', function() {
            $('.preloader').fadeOut(500);
        });
    }

    // Initialize preloader if it exists
    if($('.preloader').length > 0) {
        initPreloader();
    }

    // Mobile menu enhancements
    $('.navbar-toggler').on('click', function() {
        $(this).toggleClass('active');
    });

    // Service card hover effects
    $('.service-card').hover(
        function() {
            $(this).find('.service-icon').addClass('bounce');
        },
        function() {
            $(this).find('.service-icon').removeClass('bounce');
        }
    );

    // Team member card effects
    $('.team-member').hover(
        function() {
            $(this).find('.team-photo').addClass('scale-up');
        },
        function() {
            $(this).find('.team-photo').removeClass('scale-up');
        }
    );

    // Initialize everything on page load
    $(window).on('load', function() {
        $('body').addClass('loaded');
        
        // Trigger scroll event to set initial nav state
        $(window).trigger('scroll');
        
        // Initialize any remaining animations
        setTimeout(function() {
            $('.fade-in:not(.active)').addClass('active');
        }, 100);
    });
});

// Utility functions
function debounce(func, wait, immediate) {
    var timeout;
    return function() {
        var context = this, args = arguments;
        var later = function() {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };
        var callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow) func.apply(context, args);
    };
}

function throttle(func, limit) {
    var inThrottle;
    return function() {
        var args = arguments;
        var context = this;
        if (!inThrottle) {
            func.apply(context, args);
            inThrottle = true;
            setTimeout(() => inThrottle = false, limit);
        }
    }
}

// Expose functions globally if needed
window.SVM = {
    showNotification: function(type, message, duration) {
        showNotification(type, message, duration);
    },
    scrollToElement: function(selector, offset = 100) {
        var target = $(selector);
        if(target.length) {
            $('html, body').animate({
                scrollTop: target.offset().top - offset
            }, 1000, 'easeInOutCubic');
        }
    }
};
