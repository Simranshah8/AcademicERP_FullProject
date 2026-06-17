app.controller('CreateIDCardTemplateController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Create ID Card Template';
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.currentPages = {
            IdCardTemplate: 1,
        };

        $scope.searchData = {
            IdCardTemplate: '',
        };

        $scope.perPage = {
            IdCardTemplate: GlobalServices.getPerPageRow(),
        };

        let elementCounter = 0;

        const spacing = 20;
        function getNextPosition() {
            return {
                top: (elementCounter * spacing) + "px",
                left: "0px"
            };
        }

        $timeout(function () {
            createCardBaseLayout();
            const fields = document.querySelectorAll(".draggable-field");

            fields.forEach(field => {
                field.addEventListener("dragstart", function (e) {
                    const fieldName = this.getAttribute("data-field");
                    e.dataTransfer.setData("fieldName", fieldName);
                });
            });
            setupCardDropZone();
        });

        function setupCardDropZone() {
            const preview = document.getElementById("cardPreview");
            if (!preview) return;
            preview.addEventListener("dragover", function (e) {
                e.preventDefault();
            });
            preview.addEventListener("drop", function (e) {
                e.preventDefault();
                const fieldName = e.dataTransfer.getData("fieldName");
                if (!fieldName) return;
                $scope.$apply(function () {
                    $scope.addField(fieldName);
                });
            });
        }

        $scope.validDateRange = "";
        $(function () {
            $('#datepicker-1').daterangepicker({
                locale: { format: 'MM/DD/YYYY' }
            });
            $('#datepicker-1').on('apply.daterangepicker', function (ev, picker) {
                $scope.$apply(function () {
                    $scope.validDateRange = picker.startDate.format('MM/DD/YYYY') + " - " + picker.endDate.format('MM/DD/YYYY');
                });
            });
        });
        $scope.font = { selected: '' };
        $scope.fontList = [
            { name: 'Arial', value: 'Arial' },
            { name: 'Times New Roman', value: 'Times New Roman' },
            { name: 'Helvetica', value: 'Helvetica' },
            { name: 'Verdana', value: 'Verdana' },
            { name: 'Tahoma', value: 'Tahoma' },
            { name: 'Calibri', value: 'Calibri' },
            { name: 'Segoe UI', value: 'Segoe UI' },
            { name: 'Roboto', value: "'Roboto', sans-serif" },
            { name: 'Open Sans', value: "'Open Sans', sans-serif" },
            { name: 'Montserrat', value: "'Montserrat', sans-serif" },
            { name: 'Lato', value: "'Lato', sans-serif" }
        ];
        /*For Inch */
        $scope.inch = {
            width: 3.5,
            height: 2.5
        };

        $scope.applyInchSize = function () {
            const DPI = 96;
            const preview = document.getElementById("cardPreview");
            if (!$scope.inch.width || !$scope.inch.height) return;
            preview.style.width = ($scope.inch.width * DPI) + "px";
            preview.style.height = ($scope.inch.height * DPI) + "px";
        };

        function createCardBaseLayout() {
            const preview = document.getElementById("cardPreview");
            if (!preview) return;
            preview.innerHTML = "";
            const header = document.createElement("div");
            header.id = "cardHeader";
            header.style.height = $scope.card.headerHeight + "px";
            header.style.background = $scope.card.headerColor;
            header.style.color = "white";
            header.style.display = "flex";
            header.style.alignItems = "center";
            header.style.justifyContent = "center";
            header.style.position = "absolute";
            header.style.top = "0";
            header.style.left = "0";
            header.style.right = "0";
            preview.appendChild(header);

            const resizeHandle = document.createElement("div");
            resizeHandle.style.position = "absolute";
            resizeHandle.style.bottom = "0";
            resizeHandle.style.left = "0";
            resizeHandle.style.width = "100%";
            resizeHandle.style.height = "6px";
            resizeHandle.style.cursor = "ns-resize";
            resizeHandle.style.background = "transparent";

            header.appendChild(resizeHandle);
            resizeHandle.addEventListener("mousedown", function (e) {
                e.preventDefault();
                const startY = e.clientY;
                const startHeight = header.offsetHeight;
                function onMouseMove(e) {
                    let newHeight = startHeight + (e.clientY - startY);
                    if (newHeight < 30) newHeight = 30;
                    header.style.height = newHeight + "px";
                    const content = document.getElementById("cardContentArea");
                    content.style.top = newHeight + "px";

                    $scope.$applyAsync(function () {
                        $scope.card.headerHeight = newHeight;
                    });
                }
                function onMouseUp() {
                    document.removeEventListener("mousemove", onMouseMove);
                    document.removeEventListener("mouseup", onMouseUp);
                }
                document.addEventListener("mousemove", onMouseMove);
                document.addEventListener("mouseup", onMouseUp);
            });

            const content = document.createElement("div");
            content.id = "cardContentArea";
            content.style.position = "absolute";
            content.style.left = "0px";
            content.style.right = "0";
            content.style.top = $scope.card.headerHeight + "px";
            content.style.bottom = "0";
            preview.appendChild(content);
        }
        /* ================= CARD MODEL ================= */
        $scope.card = {
            backgroundColor: "#28a745",
            borderEnabled: false,
            borderSize: 2,
            borderColor: "#28a745",
            orientation: 'landscape'
        };
        $scope.card.headerHeight = 76;
        $scope.card.headerColor = "#1e8c4a";
        $scope.updateHeaderHeight = function () {
            const header = document.getElementById("cardHeader");
            const content = document.getElementById("cardContentArea");
            if (!header || !content) return;

            header.style.height = $scope.card.headerHeight + "px";
            content.style.top = $scope.card.headerHeight + "px";
        };

        $scope.updateHeaderColor = function () {
            const header = document.getElementById("cardHeader");
            if (!header) return;
            header.style.background = $scope.card.headerColor;
        };

        //***********Bar Code ****************
        $scope.barcodeValue = ''; // initialize
        $scope.addBarcode = function () {
            if (!$scope.barcodeValue) {
                alert("Enter barcode value");
                return;
            }
            const container = document.getElementById("cardPreview");
            if (!container) {
                alert("cardPreview not found!");
                return;
            }
            const barcodeDiv = document.createElement("div");
            barcodeDiv.className = "draggable-item resizable-item";
            barcodeDiv.setAttribute("data-is-barcode", "true");
            barcodeDiv.style.top = "50px";
            barcodeDiv.style.left = "20px";
            barcodeDiv.style.width = "150px";
            barcodeDiv.style.height = "50px";
            barcodeDiv.style.background = "transparent";
            barcodeDiv.style.userSelect = "none";

            const deleteBtn = document.createElement("div");
            deleteBtn.className = "image-remove";
            deleteBtn.innerHTML = "X";
            deleteBtn.onclick = function (e) {
                e.stopPropagation();
                barcodeDiv.remove();
            };
            barcodeDiv.appendChild(deleteBtn);
            const svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
            svg.setAttribute("width", "100%");
            svg.setAttribute("height", "100%");

            barcodeDiv.appendChild(svg);
            container.appendChild(barcodeDiv);
            JsBarcode(svg, $scope.barcodeValue, {
                format: "CODE128",
                width: 1,
                height: 30,
                displayValue: true,
                fontSize: 10
            });

            makeDraggable(barcodeDiv);
            const observer = new ResizeObserver(() => {
                const w = barcodeDiv.offsetWidth;
                const h = barcodeDiv.offsetHeight;
                svg.setAttribute("width", w);
                svg.setAttribute("height", h);

                JsBarcode(svg, $scope.barcodeValue, {
                    format: "CODE128",
                    width: Math.max(1, w / Math.max(10, $scope.barcodeValue.length * 6)),
                    height: Math.max(30, h * 0.2),
                    displayValue: true,
                    fontSize: Math.max(8, h / 5)
                });
            });
            observer.observe(barcodeDiv);
            $scope.barcodeValue = '';
            $scope.$applyAsync();
        };
        /******************** Water Mark *************/
        $scope.watermarkPreview = null;
        $scope.previewWatermark = function (input) {
            if (input.files && input.files[0]) {
                const reader = new FileReader();
                reader.onload = (e) => {
                    $scope.watermarkPreview = e.target.result;
                    $scope.$apply();
                };
                reader.readAsDataURL(input.files[0]);
            }
        };
        // Apply watermark to card
        $scope.applyWatermark = function () {
            if (!$scope.watermarkPreview) return;
            const container = document.getElementById("cardPreview");
            const header = document.getElementById("cardHeader");
            let watermarkDiv = container.querySelector(".watermark-item");
            if (!watermarkDiv) {
                watermarkDiv = document.createElement("div");
                watermarkDiv.className = "watermark-item";
                const img = document.createElement("img");
                img.src = $scope.watermarkPreview;
                watermarkDiv.appendChild(img);
                container.appendChild(watermarkDiv);
            } else {
                watermarkDiv.querySelector("img").src = $scope.watermarkPreview;
            }

            const headerHeight = header ? header.offsetHeight : 0;
            watermarkDiv.style.top = (headerHeight + 10) + "px";
        };
        const header = document.getElementById("cardHeader");
        const container = document.getElementById("cardPreview");
        const observer = new MutationObserver(() => {
            const watermarkDiv = container.querySelector(".watermark-item");
            if (watermarkDiv && header) {
                watermarkDiv.style.top = (header.offsetHeight + 10) + "px";
            }
        });
        observer.observe(header, { attributes: true, childList: true, subtree: true });
        /* ================= ADD FIELD ================= */

        $scope.addField = function (text) {

            let labelText = text;
            let fieldValue = "";

            /* ================= SCHOOL NAME ================= */
            if (text === "SchoolName") {
                const container = document.getElementById("cardPreview");
                if (container.querySelector(".school-name")) return;

                const schoolDiv = document.createElement("div");
                schoolDiv.className = "preview-item school-name draggable-item";
                schoolDiv.contentEditable = "true";

                schoolDiv.setAttribute("data-free-move", "true");
                schoolDiv.setAttribute("data-is-header", "true");
                schoolDiv.setAttribute("data-placeholder", "Enter School Name");

                schoolDiv.style.fontSize = "16px";
                schoolDiv.style.fontWeight = "bold";
                schoolDiv.style.color = "white";
                schoolDiv.style.background = "transparent";
                schoolDiv.style.border = "none";
                schoolDiv.style.outline = "none";
                schoolDiv.style.whiteSpace = "nowrap";

                schoolDiv.style.padding = "0px";
                schoolDiv.style.margin = "0px";
                schoolDiv.style.lineHeight = "1.1";
                schoolDiv.style.display = "inline-block";
                schoolDiv.style.position = "absolute"; // ✅ IMPORTANT (for remove button position)

                schoolDiv.style.top = "10px";
                schoolDiv.style.left = "80px";
                schoolDiv.style.cursor = "move";

                schoolDiv.addEventListener("click", function () {
                    schoolDiv.focus();
                    selectElement(schoolDiv);
                });

                container.appendChild(schoolDiv);
                makeDraggable(schoolDiv);
                return;
            }

            /* ================= CARD LABEL NAME ================= */
            if (text === "CardLabelName") {
                const container = document.getElementById("cardPreview");
                const labelDiv = document.createElement("div");
                labelDiv.className = "draggable-item";
                labelDiv.setAttribute("data-is-label", "true");
                labelDiv.style.display = "flex";
                labelDiv.style.alignItems = "center";
                labelDiv.style.justifyContent = "center";

                labelDiv.style.position = "absolute";
                labelDiv.style.top = "40px";
                labelDiv.style.left = "40px";
                labelDiv.style.cursor = "move";
                labelDiv.style.padding = "0px";
                labelDiv.style.borderRadius = "4px";
                labelDiv.style.background = "red";
                const input = document.createElement("input");
                input.type = "text";
                input.placeholder = "Enter Card Label";

                input.style.border = "none";
                input.style.outline = "none";
                input.style.background = "transparent";
                input.style.color = "White";
                input.style.fontWeight = "bold";
                input.style.textAlign = "center";
                input.addEventListener("mousedown", function (e) {
                    e.stopPropagation();
                });
                labelDiv.appendChild(input);

                // ✅ ADD HOVER REMOVE BUTTON HERE
                labelDiv.appendChild(createRemoveButton(labelDiv));

                labelDiv.onclick = function () {
                    selectElement(labelDiv);
                };
                const resizeHandle = document.createElement("div");
                resizeHandle.style.width = "12px";
                resizeHandle.style.height = "12px";
                resizeHandle.style.position = "absolute";
                resizeHandle.style.right = "0";
                resizeHandle.style.bottom = "0";
                resizeHandle.style.cursor = "se-resize";
                resizeHandle.style.borderRadius = "2px";

                labelDiv.style.minWidth = "60px";
                labelDiv.style.minHeight = "30px";
                labelDiv.style.boxSizing = "border-box";
                labelDiv.appendChild(resizeHandle);
                resizeHandle.addEventListener("mousedown", function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                    const startX = e.clientX;
                    const startY = e.clientY;
                    const startWidth = labelDiv.offsetWidth;
                    const startHeight = labelDiv.offsetHeight;
                    function onMouseMove(e) {
                        const newWidth = startWidth + (e.clientX - startX);
                        const newHeight = startHeight + (e.clientY - startY);
                        labelDiv.style.width = newWidth + "px";
                        labelDiv.style.height = newHeight + "px";
                    }
                    function onMouseUp() {
                        document.removeEventListener("mousemove", onMouseMove);
                        document.removeEventListener("mouseup", onMouseUp);
                    }
                    document.addEventListener("mousemove", onMouseMove);
                    document.addEventListener("mouseup", onMouseUp);
                });
                container.appendChild(labelDiv);
                makeDraggable(labelDiv);
                return;
            }

            /* ================= NORMAL FIELDS ================= */

            if (text.includes(":")) {
                const parts = text.split(":");
                labelText = parts[0];
                fieldValue = parts[1].trim();
            }

            const container = document.getElementById("cardContentArea");

            // ❌ Common Remove Button
            function createRemoveButton(parent) {
                const btn = document.createElement("div");
                btn.innerHTML = "✖";

                btn.style.position = "absolute";
                btn.style.top = "-6px";
                btn.style.right = "-6px";
                btn.style.background = "#1f7a6b";
                btn.style.color = "#fff";
                btn.style.width = "18px";
                btn.style.height = "18px";
                btn.style.borderRadius = "50%";
                btn.style.fontSize = "10px";
                btn.style.alignItems = "center";
                btn.style.justifyContent = "center";
                btn.style.cursor = "pointer";

                // ❌ Initially hidden
                btn.style.display = "none";

                btn.onclick = function (e) {
                    e.stopPropagation();
                    parent.remove();
                };

                // 👇 SHOW on hover
                parent.addEventListener("mouseenter", function () {
                    btn.style.display = "flex";
                });

                // 👇 HIDE on leave
                parent.addEventListener("mouseleave", function () {
                    btn.style.display = "none";
                });

                return btn;
            }

            const pos = getNextPosition();

            /* ================= LABEL BOX ================= */
            const labelBox = document.createElement("div");
            labelBox.className = "preview-item draggable-item";

            labelBox.style.position = "absolute";
            labelBox.style.top = pos.top;
            labelBox.style.left = pos.left;

            labelBox.style.cursor = "move";

            const label = document.createElement("span");
            label.innerText = labelText + ":";
            label.style.fontWeight = "300";

            labelBox.appendChild(createRemoveButton(labelBox));
            labelBox.appendChild(label);

            container.appendChild(labelBox);
            makeDraggable(labelBox);

            /* ================= INPUT BOX ================= */
            const inputBox = document.createElement("div");
            inputBox.className = "preview-item draggable-item";

            inputBox.style.position = "absolute";
            inputBox.style.top = pos.top;
            inputBox.style.left = (parseInt(pos.left) + 80) + "px";

            inputBox.style.cursor = "move";

            const input = document.createElement("input");
            input.type = "text";

            if (fieldValue) {
                input.value = fieldValue;
                input.readOnly = true;
            } else {
                input.placeholder = "Enter " + labelText;
            }

            input.style.border = "none";
            input.style.outline = "none";
            input.style.background = "transparent";
            input.style.minWidth = "40px";

            input.addEventListener("mousedown", function (e) {
                e.stopPropagation();
            });

            input.addEventListener("input", function () {
                this.style.width = "auto";
                const temp = document.createElement("span");
                temp.style.visibility = "hidden";
                temp.style.whiteSpace = "nowrap";
                temp.style.font = window.getComputedStyle(this).font;
                temp.innerText = this.value || this.placeholder;
                document.body.appendChild(temp);
                this.style.width = (temp.offsetWidth + 3) + "px";
                document.body.removeChild(temp);
            });

            inputBox.appendChild(createRemoveButton(inputBox));
            inputBox.appendChild(input);

            container.appendChild(inputBox);
            makeDraggable(inputBox);

            elementCounter++;
        };
        /* ================= ADD CUSTOM TEXT ================= */
        $scope.addCustomText = function () {
            if (!$scope.customText) return;
            $scope.addField($scope.customText);
            $scope.customText = "";
        };

        /* ================= ADD IMAGE ================= */
        $scope.addImage = function (files) {
            if (!files || !files[0]) return;

            const reader = new FileReader();
            reader.onload = function (e) {
                const container = document.getElementById("cardPreview");
                const oldWrapper = container.querySelector(".resizable-item.photo-style");
                if (oldWrapper) oldWrapper.remove();
                const wrapper = document.createElement("div");
                wrapper.className = "resizable-item photo-style draggable-item";

                wrapper.style.position = "absolute";
                wrapper.style.top = "15px";
                wrapper.style.left = "20px";
                wrapper.style.width = "100px";
                wrapper.style.height = "120px";
                const img = document.createElement("img");
                img.src = e.target.result;

                const removeBtn = document.createElement("div");
                removeBtn.className = "image-remove";
                removeBtn.innerHTML = "✖";
                removeBtn.onclick = function () {
                    wrapper.remove();
                    document.getElementById("imageUpload").value = "";
                };

                wrapper.appendChild(img);
                wrapper.appendChild(removeBtn);
                container.appendChild(wrapper);

                makeImageDraggable(wrapper);
            };
            reader.readAsDataURL(files[0]);
        };
        /* ================= changeOrientation ================= */
        $scope.changeOrientation = function () {
            if ($scope.card.orientation === 'landscape') {
                $scope.inch.width = 3.5;
                $scope.inch.height = 2.5;
            }
            else if ($scope.card.orientation === 'portrait') {
                $scope.inch.width = 2.5;
                $scope.inch.height = 3.5;
            }
            $scope.applyInchSize();
        };
        /* ================= BACKGROUND ================= */
        $scope.applyBackgroundColor = function () {
            const preview = document.getElementById("cardPreview");
            preview.style.backgroundImage = "none";
            preview.style.backgroundColor = $scope.card.backgroundColor;
        };
        /* ================= BORDER ================= */
        $scope.toggleBorder = function () {
            updateBorderStyle();
        };

        $scope.updateBorder = function () {
            updateBorderStyle();
        };

        function updateBorderStyle() {
            const preview = document.getElementById("cardPreview");
            if ($scope.card.borderEnabled) {
                preview.style.border =
                    $scope.card.borderSize +
                    "px solid " +
                    $scope.card.borderColor;
            } else {
                preview.style.border = "1px solid #ccc";
            }
        }

        /* =================Image Movable FUNCTION ================= */
        function makeImageDraggable(el) {
            const container = document.getElementById("cardPreview");
            el.addEventListener("mousemove", function (e) {
                const rect = el.getBoundingClientRect();
                const offsetX = e.clientX - rect.left;
                const offsetY = e.clientY - rect.top;
                if (offsetX > el.offsetWidth - 20 && offsetY > el.offsetHeight - 20) {
                    el.style.cursor = "se-resize";
                } else {
                    el.style.cursor = "move";
                }
            });
            el.onmousedown = function (e) {
                e.preventDefault();
                const rect = el.getBoundingClientRect();
                const containerRect = container.getBoundingClientRect();
                const offsetX = e.clientX - rect.left;
                const offsetY = e.clientY - rect.top;

                const resizeMode =
                    offsetX > el.offsetWidth - 20 &&
                    offsetY > el.offsetHeight - 20;
                const startX = e.clientX;
                const startY = e.clientY;

                const startWidth = el.offsetWidth;
                const startHeight = el.offsetHeight;
                const shiftX = e.clientX - rect.left;
                const shiftY = e.clientY - rect.top;
                document.onmousemove = function (e) {
                    if (resizeMode) {
                        let newWidth = startWidth + (e.clientX - startX);
                        let newHeight = startHeight + (e.clientY - startY);
                        newWidth = Math.max(40, newWidth);
                        newHeight = Math.max(40, newHeight);

                        const maxWidth = container.clientWidth - el.offsetLeft;
                        const maxHeight = container.clientHeight - el.offsetTop;
                        newWidth = Math.min(newWidth, maxWidth);
                        newHeight = Math.min(newHeight, maxHeight);
                        if (el.classList.contains("school-logo")) {
                            const size = Math.min(newWidth, newHeight);
                            el.style.width = size + "px";
                            el.style.height = size + "px";
                        } else {
                            el.style.width = newWidth + "px";
                            el.style.height = newHeight + "px";
                        }
                    } else {
                        let left = e.clientX - containerRect.left - shiftX;
                        let top = e.clientY - containerRect.top - shiftY;
                        const maxLeft = container.clientWidth - el.offsetWidth;
                        const maxTop = container.clientHeight - el.offsetHeight;

                        left = Math.max(0, Math.min(left, maxLeft));
                        top = Math.max(0, Math.min(top, maxTop));
                        el.style.left = left + "px";
                        el.style.top = top + "px";
                    }
                };
                document.onmouseup = function () {
                    document.onmousemove = null;
                    document.onmouseup = null;
                };
            };
        }
        /* ================= DRAG FUNCTION ================= */
        function makeDraggable(el) {
            const container = document.getElementById("cardPreview");
            const headerHeight = $scope.card.headerHeight || 0;

            if (el.classList.contains("resizable-item")) {
                el.style.height = el.offsetHeight + "px";
                el.style.width = el.offsetWidth + "px";
            }
            if (!el.isContentEditable) {
                el.style.userSelect = "none";
            }

            el.onmousedown = function (e) {
                if (el.isContentEditable && document.activeElement === el) {
                    if (!el.getAttribute("data-free-move")) {  // ✅ allow free-move elements
                        return;
                    }
                }
                e.preventDefault();
                const containerRect = container.getBoundingClientRect();
                const shiftX = e.clientX - el.getBoundingClientRect().left;
                const shiftY = e.clientY - el.getBoundingClientRect().top;
                function onMouseMove(e) {
                    let left = e.clientX - containerRect.left - shiftX;
                    let top = e.clientY - containerRect.top - shiftY;
                    if (!el.getAttribute("data-is-signature") &&
                        !el.getAttribute("data-is-label") &&
                        !el.getAttribute("data-is-barcode") &&
                        !el.getAttribute("data-is-header")) {
                        top -= headerHeight;
                    }
                    const maxLeft = container.clientWidth - el.offsetWidth;
                    const maxTop = container.clientHeight - el.offsetHeight;
                    left = Math.max(0, Math.min(left, maxLeft));
                    top = Math.max(0, Math.min(top, maxTop));
                    el.style.left = left + "px";
                    el.style.top = top + "px";
                }
                document.addEventListener("mousemove", onMouseMove);
                document.addEventListener("mouseup",
                    function () {
                        document.removeEventListener("mousemove", onMouseMove);
                        const elRect = el.getBoundingClientRect();
                        const containerRect = container.getBoundingClientRect();
                        const isOutside =
                            elRect.right < containerRect.left ||
                            elRect.left > containerRect.right ||
                            elRect.bottom < containerRect.top ||
                            elRect.top > containerRect.bottom;
                        if (isOutside) {
                            el.remove();
                        }
                    },
                    { once: true }
                );
            };
            el.ondragstart = () => false;
        }
        /* ================= Reposition Image Inside the card ================= */

        $scope.schoolLogoPreview = null;
        $scope.previewSchoolLogo = function (input) {
            if (!input.files || !input.files[0]) return;

            const reader = new FileReader();
            reader.onload = function (e) {
                $scope.$apply(function () {
                    $scope.schoolLogoPreview = e.target.result;
                });
            };
            reader.readAsDataURL(input.files[0]);
        };

        $scope.addSchoolLogo = function () {
            if (!$scope.schoolLogoPreview) return;
            const container = document.getElementById("cardPreview");
            if (!container) return;
            const oldLogo = container.querySelector(".logo-wrapper");
            if (oldLogo) oldLogo.remove();
            const wrapper = document.createElement("div");
            wrapper.className = "resizable-item logo-style";
            wrapper.style.top = "10px";
            wrapper.style.left = "10px";
            wrapper.style.width = "53px";
            wrapper.style.height = "53px";

            const logo = document.createElement("img");
            logo.src = $scope.schoolLogoPreview;
            wrapper.appendChild(logo);

            const removeBtn = document.createElement("div");
            removeBtn.className = "image-remove";
            removeBtn.innerHTML = "✖";
            removeBtn.onclick = function () {
                wrapper.remove();
                $scope.schoolLogoPreview = null;
                document.getElementById("schoolLogoInput").value = "";
                $scope.$apply();
            };

            wrapper.appendChild(removeBtn);
            container.appendChild(wrapper);
            makeImageDraggable(wrapper);
        };

        // Apply Font Function
        $scope.applyFont = function () {
            document.execCommand("fontName", false, $scope.font.selected);
        };
        //Apply For Font Weight
        $scope.$watch('card.fontWeight', function (newVal) {
            if (!newVal) return;
            document.execCommand("bold");
        });
        //Apply For FontSize
        $scope.applyFontSize = function () {
            document.execCommand("styleWithCSS", false, true);
            document.execCommand("fontSize", false, "7");
            const fonts = document.getElementsByTagName("font");
            for (let i = 0; i < fonts.length; i++) {
                if (fonts[i].size == "7") {
                    fonts[i].removeAttribute("size");
                    fonts[i].style.fontSize = $scope.card.fontSize + "px";
                }
            }
        };
        //Apply For Color
        $scope.applyFontColor = function () {
            document.execCommand("foreColor", false, $scope.card.fontColor);
        };
        //Apply For Bold
        $scope.applyBold = function () {
            document.execCommand("bold");
        };
        //Apply For Italic
        $scope.applyItalic = function () {
            document.execCommand("italic");
        };
        //Signature
        $scope.addSignature = function () {
            if (!$scope.signatureText) return;
            const container = document.getElementById("cardPreview");
            const sig = document.createElement("div");
            sig.innerText = $scope.signatureText;

            sig.style.position = "absolute";
            sig.style.left = "80px";
            sig.style.top = "120px";
            sig.style.cursor = "move";
            sig.style.fontFamily = "cursive";
            sig.style.fontSize = "18px";
            sig.style.borderBottom = "1px solid black";
            sig.style.padding = "3px 8px";

            sig.classList.add("draggable-item");
            sig.setAttribute("data-is-signature", "true");
            sig.onclick = function () {
                selectElement(sig);
            };
            container.appendChild(sig);
            makeDraggable(sig);
            $scope.signatureText = "";
        };

        $scope.addSignatureImage = function (files) {
            if (!files || !files[0]) return;
            const reader = new FileReader();
            reader.onload = function (e) {
                const container = document.getElementById("cardPreview");
                const wrapper = document.createElement("div");
                wrapper.style.position = "absolute";
                wrapper.style.left = "80px";
                wrapper.style.top = "120px";
                wrapper.style.cursor = "move";

                const img = document.createElement("img");
                img.src = e.target.result;
                img.style.width = "120px";
                img.style.height = "50px";
                img.style.objectFit = "contain";

                wrapper.appendChild(img);
                wrapper.classList.add("draggable-item");
                wrapper.setAttribute("data-is-signature", "true");
                wrapper.onclick = function () {
                    selectElement(wrapper);
                };
                container.appendChild(wrapper);
                makeDraggable(wrapper);
            };
            reader.readAsDataURL(files[0]);
        };

        $scope.rotateSelected = function () {
            if (!$scope.selectedElement) {
                alert("Please select rotation field first");
                return;
            }
            let currentRotation =
                parseInt($scope.selectedElement.getAttribute("data-rotation")) || 0;
            currentRotation += 15;
            $scope.selectedElement.style.transform =
                "rotate(" + currentRotation + "deg)";
            $scope.selectedElement.setAttribute("data-rotation", currentRotation);
        };

        $scope.selectedElement = null;
        function selectElement(el) {
            document.querySelectorAll(".draggable-item")
                .forEach(e => e.style.outline = "none");
            $scope.selectedElement = el;
        }

    };

    $scope.goToCreateTab = function () {
        $('#createidcardtemplate-tab').tab('show');
    };


});


