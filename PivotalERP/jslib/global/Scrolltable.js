//Added By Suresh for table column resize starts
app.directive('makeTableResizable', function ($timeout, $window) {
    return {
        restrict: 'A',
        // Scope is now empty as it doesn't need input from a controller
        scope: {},
        link: function (scope, element, attrs) {
            const storageKey = 'myDynamicTableColumnWidths';

            // --- STATE MANAGEMENT ---
            let currentWidths = {};
            let originalWidths = {};
            let hasCapturedOriginals = false;

            // --- CONFIGURATION ---
            const defaultWidths = {
                1: 300, // Particulars
                3: 150, // Description
                5: 150, // Sales Ledger
                7: 150, // Godown
            };

            // --- CORE FUNCTIONS (Unchanged) ---
            function captureOriginalWidths() {
                if (hasCapturedOriginals) return;
                const headers = element.find('thead tr:first > th');
                headers.each(function (index) {
                    originalWidths[index] = $(this).outerWidth();
                });
                hasCapturedOriginals = true;
            }

            function applyWidth(oneBasedColumnIndex, newWidth) {
                const widthValue = (typeof newWidth === 'number' && newWidth > 0) ? newWidth + 'px' : '';
                element.find(`tr > th:nth-child(${oneBasedColumnIndex})`).css('width', widthValue);
                element.find(`tbody tr > td:nth-child(${oneBasedColumnIndex})`).css('width', widthValue);
                element.find(`tfoot tr > td:nth-child(${oneBasedColumnIndex})`).css('width', widthValue);
            }

            function resetWidths() {
                const allHeaders = element.find('thead tr:first > th');
                allHeaders.each(function (index) {
                    if (defaultWidths.hasOwnProperty(index)) {
                        applyWidth(index + 1, defaultWidths[index]);
                    } else if (currentWidths.hasOwnProperty(index) && originalWidths.hasOwnProperty(index)) {
                        applyWidth(index + 1, originalWidths[index]);
                    }
                });
                currentWidths = {};
                $window.localStorage.removeItem(storageKey);
                initializeResizer();
                alert('Column layout has been reset.');
            }

            function saveWidths() {
                $window.localStorage.setItem(storageKey, angular.toJson(currentWidths));
                alert('Column layout has been saved!');
            }

            function initializeResizer() {
                $(document).off('.resizer');
                element.find('.resize-handle').remove();
                const headers = element.find('thead tr:first > th');
                for (const index in currentWidths) {
                    if (currentWidths.hasOwnProperty(index)) {
                        applyWidth(parseInt(index, 10) + 1, currentWidths[index]);
                    }
                }
                headers.filter(':visible:not(:last-child)').each(function () {
                    const $header = $(this);
                    /*$header.css('position', 'relative');*/
                    const $handle = $('<div class="resize-handle"></div>').css({
                        position: 'absolute', right: 0, top: 0, height: '100%',
                        width: '5px', cursor: 'col-resize', zIndex: 10
                    });
                    $header.append($handle);
                    $handle.on('mousedown', function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        const $currentColumn = $(this).parent();
                        const columnIndex = headers.index($currentColumn);
                        const startX = e.pageX;
                        const startWidth = $currentColumn.outerWidth();
                        $(document).on('mousemove.resizer', function (e) {
                            const newWidth = startWidth + (e.pageX - startX);
                            if (newWidth > 40) {
                                applyWidth(columnIndex + 1, newWidth);
                            }
                        });
                        $(document).on('mouseup.resizer', function (e) {
                            $(document).off('.resizer');
                            const finalWidth = $currentColumn.outerWidth();
                            currentWidths[columnIndex] = finalWidth;
                        });
                    });
                });
            }

            // --- WATCHERS & INITIALIZATION ---

            function loadAndInitialize() {
                const fromStorage = $window.localStorage.getItem(storageKey);
                currentWidths = fromStorage ? angular.fromJson(fromStorage) : {};
                $timeout(() => {
                    captureOriginalWidths();
                    initializeResizer();
                });
            }

            // Watch for ng-hide/ng-repeat changes
            scope.$watch(() => element.find('thead th:visible').length, (newValue, oldValue) => {
                if (newValue > 0 && newValue !== oldValue) {
                    loadAndInitialize();
                }
            });

            // ** NEW: INTERNAL EVENT HANDLING **
            // Find the action dropdown within the table and handle its change event.
            const $actionDropdown = element.find('#table-action-select');
            $actionDropdown.on('change', function () {
                const selectedAction = $(this).val();

                if (selectedAction === 'save') {
                    saveWidths();
                } else if (selectedAction === 'reset') {
                    resetWidths();
                }

                // Reset the dropdown to the placeholder text
                $(this).val('');
            });

            // Initial run
            loadAndInitialize();
        }
    };
});

angular.element(document).ready(function () {
    const $scrollContainer = $('.table-h-scrollbar-fix');
    const $table = $('.main-table');
    function checkScroll() {
        const scrollLeft = $scrollContainer.scrollLeft();
        const scrollTop = $scrollContainer.scrollTop();

        const isHorizontallyOverflowing = $scrollContainer[0].scrollWidth > $scrollContainer[0].clientWidth;
        const isVerticallyOverflowing = $scrollContainer[0].scrollHeight > $scrollContainer[0].clientHeight;

        const $highlightThs = $table.find('th.highlight-on-scroll');
        const $highlightTds = $table.find('td.highlight-on-scroll');

        if (isHorizontallyOverflowing && scrollLeft > 0) {
            $highlightThs.addClass('scrolled');
            $highlightTds.addClass('scrolled');
            $table.addClass('has-scroll-main');
        } else {
            $highlightThs.removeClass('scrolled');
            $highlightTds.removeClass('scrolled');
            $table.removeClass('has-scroll-main');
        }

        // Handle vertical scroll class
        if (isVerticallyOverflowing && scrollTop > 0) {
            $table.addClass('vertical-scroll');
        } else {
            $table.removeClass('vertical-scroll');
        }
    }

    checkScroll();

    $scrollContainer.on('scroll', function () {
        checkScroll();
    });

    $(window).on('resize', function () {
        checkScroll();
    });
});