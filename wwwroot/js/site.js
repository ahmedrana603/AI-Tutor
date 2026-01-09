// AI Learning Platform JavaScript
document.addEventListener('DOMContentLoaded', function() {
    initializeAnimations();
    initializeInteractiveFeatures();
    initializePlagiarismChecker();
    initializeLearningProgress();
});

// Initialize animations and scroll effects
function initializeAnimations() {
    // Intersection Observer for animations
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver(function(entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-in');
            }
        });
    }, observerOptions);

    // Observe elements for animation
    document.querySelectorAll('.feature-card, .tutor-card, .learning-path-card, .plagiarism-demo').forEach(el => {
        observer.observe(el);
    });

    // Add hover effects for cards
    document.querySelectorAll('.tutor-card, .learning-path-card, .feature-card').forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-10px)';
            this.style.transition = 'all 0.3s ease';
        });
        
        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0)';
        });
    });
}

// Initialize interactive features
function initializeInteractiveFeatures() {
    // AI Tutor session starter
    document.querySelectorAll('.tutor-session-btn').forEach(btn => {
        btn.addEventListener('click', function(e) {
            e.preventDefault();
            const tutorId = this.dataset.tutorId;
            startTutorSession(tutorId);
        });
    });

    // Learning path starter
    document.querySelectorAll('.learning-path-btn').forEach(btn => {
        btn.addEventListener('click', function(e) {
            e.preventDefault();
            const pathId = this.dataset.pathId;
            startLearningPath(pathId);
        });
    });

    // Smooth scrolling for anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function(e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
}

// Start AI Tutor Session
function startTutorSession(tutorId) {
    const btn = document.querySelector(`[data-tutor-id="${tutorId}"]`);
    const originalText = btn.innerHTML;
    
    // Show loading state
    btn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Connecting...';
    btn.disabled = true;

    // Simulate API call
    fetch('/AITutor/StartSession', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        },
        body: JSON.stringify({ tutorId: parseInt(tutorId) })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            btn.innerHTML = '<i class="fas fa-check me-2"></i>Session Started!';
            btn.classList.remove('btn-primary');
            btn.classList.add('btn-success');
            
            // Show success message
            showNotification('AI Tutor session started successfully!', 'success');
            
            // Redirect to session page after delay
            setTimeout(() => {
                window.location.href = `/AITutor/Session/${data.sessionId}`;
            }, 2000);
        } else {
            throw new Error('Failed to start session');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        btn.innerHTML = originalText;
        btn.disabled = false;
        showNotification('Failed to start session. Please try again.', 'error');
    });
}

// Start Learning Path
function startLearningPath(pathId) {
    const btn = document.querySelector(`[data-path-id="${pathId}"]`);
    const originalText = btn.innerHTML;
    
    // Show loading state
    btn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Starting...';
    btn.disabled = true;

    // Simulate API call
    fetch('/Learning/StartLearning', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        },
        body: JSON.stringify({ pathId: parseInt(pathId) })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            btn.innerHTML = '<i class="fas fa-check me-2"></i>Started!';
            btn.classList.remove('btn-success');
            btn.classList.add('btn-primary');
            
            // Show success message
            showNotification('Learning path started successfully!', 'success');
            
            // Redirect to learning page after delay
            setTimeout(() => {
                window.location.href = `/Learning/Path/${pathId}`;
            }, 2000);
        } else {
            throw new Error('Failed to start learning path');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        btn.innerHTML = originalText;
        btn.disabled = false;
        showNotification('Failed to start learning path. Please try again.', 'error');
    });
}

// Initialize Plagiarism Checker
function initializePlagiarismChecker() {
    const checkForm = document.getElementById('plagiarism-check-form');
    if (checkForm) {
        checkForm.addEventListener('submit', function(e) {
            e.preventDefault();
            checkPlagiarism();
        });
    }

    // Real-time character count
    const contentTextarea = document.getElementById('content');
    const charCount = document.getElementById('char-count');
    if (contentTextarea && charCount) {
        contentTextarea.addEventListener('input', function() {
            const count = this.value.length;
            charCount.textContent = count;
            
            // Change color based on length
            if (count < 100) {
                charCount.className = 'text-danger';
            } else if (count < 500) {
                charCount.className = 'text-warning';
            } else {
                charCount.className = 'text-success';
            }
        });
    }
}

// Check Plagiarism
function checkPlagiarism() {
    const form = document.getElementById('plagiarism-check-form');
    const content = document.getElementById('content').value;
    const title = document.getElementById('title').value;
    const submitBtn = document.getElementById('check-btn');
    const resultsDiv = document.getElementById('plagiarism-results');

    if (!content.trim()) {
        showNotification('Please enter some content to check.', 'warning');
        return;
    }

    // Show loading state
    const originalText = submitBtn.innerHTML;
    submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Analyzing...';
    submitBtn.disabled = true;

    // Hide previous results
    if (resultsDiv) {
        resultsDiv.style.display = 'none';
    }

    // Simulate API call
    fetch('/Plagiarism/Analyze', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        },
        body: JSON.stringify({ content: content, title: title })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            displayPlagiarismResults(data.report);
            showNotification('Plagiarism analysis completed!', 'success');
        } else {
            throw new Error('Analysis failed');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showNotification('Failed to analyze document. Please try again.', 'error');
    })
    .finally(() => {
        submitBtn.innerHTML = originalText;
        submitBtn.disabled = false;
    });
}

// Display Plagiarism Results
function displayPlagiarismResults(report) {
    const resultsDiv = document.getElementById('plagiarism-results');
    if (!resultsDiv) return;

    const similarityClass = report.similarityScore < 10 ? 'text-success' : 
                           report.similarityScore < 20 ? 'text-warning' : 'text-danger';
    
    const statusClass = report.similarityScore < 10 ? 'bg-success' : 
                       report.similarityScore < 20 ? 'bg-warning' : 'bg-danger';

    resultsDiv.innerHTML = `
        <div class="card mt-4">
            <div class="card-header d-flex justify-content-between align-items-center">
                <h5 class="mb-0">Analysis Results</h5>
                <span class="badge ${statusClass}">${report.similarityScore}% Similarity</span>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <h6>Document Information</h6>
                        <p><strong>Title:</strong> ${report.documentTitle}</p>
                        <p><strong>Analysis Date:</strong> ${new Date(report.analysisDate).toLocaleString()}</p>
                        <p><strong>Status:</strong> ${report.status}</p>
                    </div>
                    <div class="col-md-6">
                        <h6>Similarity Score</h6>
                        <div class="progress mb-2">
                            <div class="progress-bar ${statusClass}" role="progressbar" 
                                 style="width: ${report.similarityScore}%" 
                                 aria-valuenow="${report.similarityScore}" 
                                 aria-valuemin="0" aria-valuemax="100">
                                ${report.similarityScore}%
                            </div>
                        </div>
                        <small class="text-muted">
                            ${report.similarityScore < 10 ? 'Excellent - Original content' : 
                              report.similarityScore < 20 ? 'Good - Minor similarities' : 
                              'Attention needed - High similarity detected'}
                        </small>
                    </div>
                </div>
                
                ${report.matches && report.matches.length > 0 ? `
                    <hr>
                    <h6>Similar Sources Found</h6>
                    <div class="matches-list">
                        ${report.matches.map(match => `
                            <div class="match-item p-3 mb-2 border rounded">
                                <div class="d-flex justify-content-between align-items-start">
                                    <div>
                                        <strong>${match.source}</strong>
                                        <br>
                                        <small class="text-muted">${match.similarity}% similarity</small>
                                    </div>
                                    <a href="${match.url}" target="_blank" class="btn btn-sm btn-outline-primary">
                                        <i class="fas fa-external-link-alt"></i>
                                    </a>
                                </div>
                                <div class="mt-2">
                                    <small class="text-muted">Matched text:</small>
                                    <p class="matched-text">"${match.matchedText}"</p>
                                </div>
                            </div>
                        `).join('')}
                    </div>
                ` : ''}
            </div>
        </div>
    `;

    resultsDiv.style.display = 'block';
    resultsDiv.scrollIntoView({ behavior: 'smooth' });
}

// Initialize Learning Progress
function initializeLearningProgress() {
    // Progress bars animation
    document.querySelectorAll('.progress-bar').forEach(bar => {
        const width = bar.style.width;
        bar.style.width = '0%';
        
        setTimeout(() => {
            bar.style.transition = 'width 2s ease-in-out';
            bar.style.width = width;
        }, 500);
    });

    // Interactive progress updates
    document.querySelectorAll('.progress-update-btn').forEach(btn => {
        btn.addEventListener('click', function() {
            const pathId = this.dataset.pathId;
            const currentProgress = parseInt(this.dataset.progress) || 0;
            const newProgress = Math.min(currentProgress + 10, 100);
            
            updateLearningProgress(pathId, newProgress);
        });
    });
}

// Update Learning Progress
function updateLearningProgress(pathId, progress) {
    fetch('/Learning/UpdateProgress', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        },
        body: JSON.stringify({ pathId: parseInt(pathId), progress: progress })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            // Update progress bar
            const progressBar = document.querySelector(`[data-path-id="${pathId}"] .progress-bar`);
            if (progressBar) {
                progressBar.style.width = `${data.newProgress}%`;
                progressBar.textContent = `${data.newProgress}%`;
            }
            
            showNotification(`Progress updated to ${data.newProgress}%!`, 'success');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showNotification('Failed to update progress.', 'error');
    });
}

// Show Notification
function showNotification(message, type = 'info') {
    // Remove existing notifications
    const existingNotifications = document.querySelectorAll('.notification');
    existingNotifications.forEach(notification => notification.remove());

    const notification = document.createElement('div');
    notification.className = `notification alert alert-${type === 'error' ? 'danger' : type} alert-dismissible fade show`;
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        z-index: 9999;
        min-width: 300px;
        box-shadow: 0 4px 12px rgba(0,0,0,0.15);
    `;
    
    notification.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;

    document.body.appendChild(notification);

    // Auto remove after 5 seconds
    setTimeout(() => {
        if (notification.parentNode) {
            notification.remove();
        }
    }, 5000);
}

// Search functionality
function initializeSearch() {
    const searchInput = document.getElementById('search-input');
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            const query = this.value.toLowerCase();
            const items = document.querySelectorAll('.searchable-item');
            
            items.forEach(item => {
                const text = item.textContent.toLowerCase();
                if (text.includes(query)) {
                    item.style.display = 'block';
                } else {
                    item.style.display = 'none';
                }
            });
        });
    }
}

// Initialize search when DOM is loaded
document.addEventListener('DOMContentLoaded', initializeSearch);

// Smooth scroll for all internal links
document.addEventListener('DOMContentLoaded', function() {
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function(e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
});

// Add loading states to all forms
document.addEventListener('DOMContentLoaded', function() {
    document.querySelectorAll('form').forEach(form => {
        form.addEventListener('submit', function() {
            const submitBtn = this.querySelector('button[type="submit"], input[type="submit"]');
            if (submitBtn && !submitBtn.disabled) {
                const originalText = submitBtn.innerHTML;
                submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Processing...';
                submitBtn.disabled = true;
                
                // Re-enable after 5 seconds as fallback
                setTimeout(() => {
                    submitBtn.innerHTML = originalText;
                    submitBtn.disabled = false;
                }, 5000);
            }
        });
    });
});

// GenAI Home interactions
(function () {
    document.addEventListener('DOMContentLoaded', function () {
        // Set background images from data-img
        document.querySelectorAll('.genai-card-media').forEach(function (el) {
            var url = el.getAttribute('data-img');
            if (url) el.style.backgroundImage = 'url(' + url + ')';
        });

        // Mouseover/mouseout to show hover info using native title tooltip
        document.querySelectorAll('.genai-card').forEach(function (card) {
            var hoverText = card.getAttribute('data-hover');
            if (hoverText) card.setAttribute('title', hoverText.replace(/<br>/g, '\n'));
        });

        // Smooth scroll for anchors
        document.querySelectorAll('a.genai-anchor[href^="#"]').forEach(function (a) {
            a.addEventListener('click', function (e) {
                var target = document.querySelector(this.getAttribute('href'));
                if (target) {
                    e.preventDefault();
                    target.scrollIntoView({ behavior: 'smooth', block: 'start' });
                }
            });
        });
    });
})();

// ================== GenAI Hover Tooltip ==================
document.addEventListener("DOMContentLoaded", function () {
    // Create tooltip div
    const tooltip = document.createElement("div");
    tooltip.className = "genai-tooltip";
    document.body.appendChild(tooltip);

    // Select all genai cards
    const cards = document.querySelectorAll(".genai-card");

    cards.forEach(card => {
        card.addEventListener("mouseenter", function () {
            const info = this.getAttribute("data-hover");
            if (info) {
                tooltip.innerHTML = info;
                tooltip.style.display = "block";
            }
        });

        card.addEventListener("mousemove", function (e) {
            tooltip.style.top = (e.pageY + 15) + "px";
            tooltip.style.left = (e.pageX + 15) + "px";
        });

        card.addEventListener("mouseleave", function () {
            tooltip.style.display = "none";
        });
    });
});


// === Observe AI Companies block (#block3) ===
(function () {
  const block = document.getElementById('block3');
  if (!block) return;

  const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.intersectionRatio >= 1) {
        // fully visible
        block.classList.remove('not-fully-visible');
      } else {
        // not fully visible
        block.classList.add('not-fully-visible');
      }
    });
  }, { threshold: [1.0] }); // trigger only when fully visible

  observer.observe(block);
})();
