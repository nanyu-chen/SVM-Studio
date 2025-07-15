// Admin Panel JavaScript
document.addEventListener('DOMContentLoaded', function() {
    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function(tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Auto-hide alerts after 5 seconds
    setTimeout(function() {
        var alerts = document.querySelectorAll('.alert-dismissible');
        alerts.forEach(function(alert) {
            var bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        });
    }, 5000);

    // Confirm delete actions
    var deleteButtons = document.querySelectorAll('.btn-delete');
    deleteButtons.forEach(function(button) {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            var itemName = this.getAttribute('data-item-name') || 'item';
            if (confirm('Are you sure you want to delete this ' + itemName + '? This action cannot be undone.')) {
                if (this.tagName === 'A') {
                    window.location.href = this.href;
                } else if (this.tagName === 'BUTTON' && this.form) {
                    this.form.submit();
                }
            }
        });
    });

    // Image preview functionality
    var imageInputs = document.querySelectorAll('input[type="file"][accept*="image"]');
    imageInputs.forEach(function(input) {
        input.addEventListener('change', function(e) {
            var file = e.target.files[0];
            if (file) {
                var reader = new FileReader();
                reader.onload = function(e) {
                    var preview = document.querySelector('#' + input.id + '-preview');
                    if (preview) {
                        preview.src = e.target.result;
                        preview.style.display = 'block';
                    }
                };
                reader.readAsDataURL(file);
            }
        });
    });

    // Form validation
    var forms = document.querySelectorAll('.needs-validation');
    forms.forEach(function(form) {
        form.addEventListener('submit', function(e) {
            if (!form.checkValidity()) {
                e.preventDefault();
                e.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
    });

    // Auto-save functionality for forms
    var autoSaveForms = document.querySelectorAll('.auto-save');
    autoSaveForms.forEach(function(form) {
        var inputs = form.querySelectorAll('input, textarea, select');
        inputs.forEach(function(input) {
            input.addEventListener('input', debounce(function() {
                saveFormData(form);
            }, 1000));
        });
    });

    // Rich text editor initialization (if using TinyMCE or similar)
    if (typeof tinymce !== 'undefined') {
        tinymce.init({
            selector: '.rich-text-editor',
            plugins: 'advlist autolink lists link image charmap print preview anchor',
            toolbar: 'undo redo | formatselect | bold italic backcolor | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | removeformat | help',
            content_style: 'body { font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif; font-size: 14px }'
        });
    }

    // Data table initialization (if using DataTables)
    if (typeof $ !== 'undefined' && $.fn.DataTable) {
        $('.data-table').DataTable({
            responsive: true,
            pageLength: 25,
            order: [[0, 'desc']],
            language: {
                search: "Search:",
                lengthMenu: "Show _MENU_ entries",
                info: "Showing _START_ to _END_ of _TOTAL_ entries",
                paginate: {
                    first: "First",
                    last: "Last",
                    next: "Next",
                    previous: "Previous"
                }
            }
        });
    }

    // Status update functionality
    var statusButtons = document.querySelectorAll('.status-update');
    statusButtons.forEach(function(button) {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            var id = this.getAttribute('data-id');
            var status = this.getAttribute('data-status');
            var url = this.getAttribute('data-url');
            
            updateStatus(id, status, url);
        });
    });

    // Bulk actions
    var selectAllCheckbox = document.querySelector('#select-all');
    var itemCheckboxes = document.querySelectorAll('.item-checkbox');
    var bulkActionButtons = document.querySelectorAll('.bulk-action');

    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener('change', function() {
            itemCheckboxes.forEach(function(checkbox) {
                checkbox.checked = selectAllCheckbox.checked;
            });
            updateBulkActionButtons();
        });
    }

    itemCheckboxes.forEach(function(checkbox) {
        checkbox.addEventListener('change', function() {
            updateBulkActionButtons();
            updateSelectAllState();
        });
    });

    bulkActionButtons.forEach(function(button) {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            var action = this.getAttribute('data-action');
            var selectedItems = getSelectedItems();
            
            if (selectedItems.length === 0) {
                alert('Please select at least one item.');
                return;
            }
            
            if (confirm('Are you sure you want to ' + action + ' ' + selectedItems.length + ' item(s)?')) {
                performBulkAction(action, selectedItems);
            }
        });
    });
});

// Utility functions
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

function saveFormData(form) {
    // Implementation for auto-save functionality
    var formData = new FormData(form);
    var statusElement = document.querySelector('.save-status');
    
    if (statusElement) {
        statusElement.textContent = 'Saving...';
        statusElement.className = 'save-status text-warning';
    }
    
    // Simulate save (replace with actual AJAX call)
    setTimeout(function() {
        if (statusElement) {
            statusElement.textContent = 'Saved';
            statusElement.className = 'save-status text-success';
        }
    }, 1000);
}

function updateStatus(id, status, url) {
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify({ id: id, status: status })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            // Update UI to reflect status change
            var statusElement = document.querySelector('[data-id="' + id + '"] .status-badge');
            if (statusElement) {
                statusElement.textContent = status;
                statusElement.className = 'badge bg-' + getStatusColor(status);
            }
            showAlert('Status updated successfully', 'success');
        } else {
            showAlert('Error updating status', 'danger');
        }
    })
    .catch(error => {
        showAlert('Error updating status', 'danger');
    });
}

function getStatusColor(status) {
    switch (status.toLowerCase()) {
        case 'active':
        case 'published':
        case 'confirmed':
            return 'success';
        case 'pending':
        case 'draft':
            return 'warning';
        case 'inactive':
        case 'cancelled':
            return 'secondary';
        case 'rejected':
            return 'danger';
        default:
            return 'primary';
    }
}

function updateBulkActionButtons() {
    var selectedItems = getSelectedItems();
    var bulkActionButtons = document.querySelectorAll('.bulk-action');
    
    bulkActionButtons.forEach(function(button) {
        button.disabled = selectedItems.length === 0;
    });
}

function updateSelectAllState() {
    var selectAllCheckbox = document.querySelector('#select-all');
    var itemCheckboxes = document.querySelectorAll('.item-checkbox');
    var checkedCount = document.querySelectorAll('.item-checkbox:checked').length;
    
    if (selectAllCheckbox) {
        selectAllCheckbox.checked = checkedCount === itemCheckboxes.length;
        selectAllCheckbox.indeterminate = checkedCount > 0 && checkedCount < itemCheckboxes.length;
    }
}

function getSelectedItems() {
    var selectedItems = [];
    var checkedBoxes = document.querySelectorAll('.item-checkbox:checked');
    
    checkedBoxes.forEach(function(checkbox) {
        selectedItems.push(checkbox.value);
    });
    
    return selectedItems;
}

function performBulkAction(action, selectedItems) {
    var url = '/Admin/BulkAction';
    
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: JSON.stringify({ action: action, items: selectedItems })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            showAlert('Bulk action completed successfully', 'success');
            // Refresh the page or update the UI
            location.reload();
        } else {
            showAlert('Error performing bulk action', 'danger');
        }
    })
    .catch(error => {
        showAlert('Error performing bulk action', 'danger');
    });
}

function showAlert(message, type) {
    var alertContainer = document.querySelector('.alert-container');
    if (!alertContainer) {
        alertContainer = document.createElement('div');
        alertContainer.className = 'alert-container';
        document.body.appendChild(alertContainer);
    }
    
    var alert = document.createElement('div');
    alert.className = 'alert alert-' + type + ' alert-dismissible fade show';
    alert.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;
    
    alertContainer.appendChild(alert);
    
    // Auto-hide after 5 seconds
    setTimeout(function() {
        var bsAlert = new bootstrap.Alert(alert);
        bsAlert.close();
    }, 5000);
}

// Export functions for use in other scripts
window.AdminPanel = {
    showAlert: showAlert,
    updateStatus: updateStatus,
    saveFormData: saveFormData
};
