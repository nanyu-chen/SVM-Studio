// SVM Studio - Clean & Elegant JavaScript

$(document).ready(function() {
    // Initialize smooth scrolling
    initSmoothScrolling();
    
    // Initialize gallery lightbox
    initGalleryLightbox();
    
    // Initialize scroll to top button
    initScrollToTop();
    
    // Initialize form enhancements
    initFormEnhancements();
    
    // Initialize navigation effects
    initNavigationEffects();
    
    // Initialize counter animations
    initCounterAnimations();
    
    // Initialize lazy loading for images
    initImageLazyLoading();
});

// Smooth scrolling for anchor links
function initSmoothScrolling() {
    $('a[href^="#"]').on('click', function(e) {
        e.preventDefault();
        
        var target = $(this.getAttribute('href'));
        if(target.length) {
            $('html, body').animate({
                scrollTop: target.offset().top - 100
            }, 800);
        }
    });
}

// Gallery lightbox functionality
function initGalleryLightbox() {
    $('.gallery-item').on('click', function(e) {
        e.preventDefault();
        
        var imgSrc = $(this).find('img').attr('src');
        var imgAlt = $(this).find('img').attr('alt');
        
        var lightboxHTML = `
            <div class="lightbox-overlay">
                <div class="lightbox-content">
                    <span class="lightbox-close">&times;</span>
                    <img src="${imgSrc}" alt="${imgAlt}">
                </div>
            </div>
        `;
        
        $('body').append(lightboxHTML);
        
        // Close lightbox on overlay click or close button
        $('.lightbox-overlay').on('click', function(e) {
            if(e.target === this || $(e.target).hasClass('lightbox-close')) {
                $(this).remove();
            }
        });
        
        // Close lightbox on ESC key
        $(document).on('keydown', function(e) {
            if(e.key === 'Escape') {
                $('.lightbox-overlay').remove();
            }
        });
    });
}

// Scroll to top button
function initScrollToTop() {
    // Create scroll to top button if it doesn't exist
    if($('.scroll-to-top').length === 0) {
        $('body').append('<button class="scroll-to-top" id="scrollToTop"><i class="fas fa-arrow-up"></i></button>');
    }
    
    var scrollToTop = $('#scrollToTop');
    
    // Show/hide scroll to top button based on scroll position
    $(window).scroll(function() {
        if($(this).scrollTop() > 300) {
            scrollToTop.fadeIn();
        } else {
            scrollToTop.fadeOut();
        }
    });
    
    // Scroll to top functionality
    scrollToTop.on('click', function() {
        $('html, body').animate({
            scrollTop: 0
        }, 600);
    });
}

// Form enhancements
function initFormEnhancements() {
    // Add floating labels effect
    $('.form-floating input, .form-floating textarea').on('focus blur', function() {
        $(this).parent().toggleClass('focused', $(this).is(':focus') || $(this).val().length > 0);
    });
    
    // Form validation feedback
    $('form').on('submit', function(e) {
        var isValid = true;
        
        // Check required fields
        $(this).find('[required]').each(function() {
            if(!$(this).val().trim()) {
                isValid = false;
                $(this).addClass('is-invalid');
            } else {
                $(this).removeClass('is-invalid');
            }
        });
        
        // Check email format
        $(this).find('input[type="email"]').each(function() {
            var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if($(this).val() && !emailPattern.test($(this).val())) {
                isValid = false;
                $(this).addClass('is-invalid');
            }
        });
        
        if(!isValid) {
            e.preventDefault();
            showNotification('error', 'Please fill in all required fields correctly.');
        }
    });
}

// Navigation effects
function initNavigationEffects() {
    var navbar = $('.navbar');
    
    // Add/remove background on scroll
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
}

// Counter animations
function initCounterAnimations() {
    $('.counter').each(function() {
        var $this = $(this);
        var countTo = $this.attr('data-count');
        
        if(countTo) {
            var observer = new IntersectionObserver(function(entries) {
                entries.forEach(function(entry) {
                    if(entry.isIntersecting) {
                        $({ countNum: 0 }).animate({
                            countNum: countTo
                        }, {
                            duration: 2000,
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

// Image lazy loading
function initImageLazyLoading() {
    if('IntersectionObserver' in window) {
        var imageObserver = new IntersectionObserver(function(entries) {
            entries.forEach(function(entry) {
                if(entry.isIntersecting) {
                    var img = entry.target;
                    img.src = img.dataset.src;
                    img.classList.remove('lazy');
                    imageObserver.unobserve(img);
                }
            });
        });
        
        document.querySelectorAll('img[data-src]').forEach(function(img) {
            imageObserver.observe(img);
        });
    }
}

// Notification system
function showNotification(type, message) {
    var notification = $(`
        <div class="alert alert-${type === 'error' ? 'danger' : type} alert-dismissible fade show notification" role="alert">
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `);
    
    // Add notification to the page
    if($('.notification-container').length === 0) {
        $('body').prepend('<div class="notification-container"></div>');
    }
    
    $('.notification-container').append(notification);
    
    // Auto-hide after 5 seconds
    setTimeout(function() {
        notification.fadeOut(function() {
            $(this).remove();
        });
    }, 5000);
}

// Initialize parallax effect for hero sections
function initParallaxEffect() {
    $(window).scroll(function() {
        var scrolled = $(this).scrollTop();
        $('.parallax-bg').css('transform', 'translateY(' + (scrolled * 0.5) + 'px)');
    });
}

// Initialize on page load
$(window).on('load', function() {
    // Initialize parallax if elements exist
    if($('.parallax-bg').length > 0) {
        initParallaxEffect();
    }
    
    // Remove any loading states
    $('.loading').removeClass('loading');
});
