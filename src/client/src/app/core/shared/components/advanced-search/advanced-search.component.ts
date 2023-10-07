import { trigger, transition, style, animate } from "@angular/animations";
import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Inject, Input, IterableDiffers, OnInit, Output, ViewChild } from "@angular/core";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { NgAsAdvancedSearchTerm, NgAsConfig, NgAsHeader, NgAsSearchTerm } from "src/app/core/models/Filters/SearchTerm";

@Component({
    selector: "app-advanced-search",
    templateUrl: "./advanced-search.component.html",
    styleUrls: ["./advanced-search.component.scss"],
    animations: [
        trigger("inOutAnimModelabelAdv", [
            transition(":enter", [style({ opacity: 0, transform: "translateY(40px)" }), animate(400, style({ opacity: 1, transform: "translateY(0px)" }))]),
            transition(":leave", [style({ opacity: 1, transform: "translateY(0px)" }), animate(400, style({ opacity: 0, transform: "translateY(40px)" }))])
        ]),
        trigger("inOutAnimModelabelBas", [
            transition(":enter", [style({ opacity: 0, transform: "translateY(-40px)" }), animate("0.4s ease-out", style({ opacity: 1, transform: "translateY(0px)" }))]),
            transition(":leave", [style({ opacity: 1, transform: "translateY(0px)" }), animate("0.4s ease-in", style({ opacity: 0, transform: "translateY(-40px)" }))])
        ]),
        trigger("inOutAnimAdvsearch", [
            transition(":enter", [style({ height: 0, opacity: 0 }), animate("0.4s ease-out", style({ height: "{{maxHeight}}px", opacity: 1 }))], {
                params: { maxHeight: 0 }
            }),
            transition(":leave", [style({ height: "{{maxHeight}}px", opacity: 1 }), animate("0.4s ease-in", style({ height: 0, opacity: 0 }))], {
                params: { maxHeight: 0 }
            })
        ])
    ]
})
export class AdvancedSearchComponent implements OnInit {
    // TODO update readme
    // TODO updat example site
    // TODO come up with a different way of getting the term height
    // TODO handle header types (especially dates)

    constructor(
        public dialogRef: MatDialogRef<AdvancedSearchComponent>,
        @Inject(MAT_DIALOG_DATA) public _data: NgAsConfig,
        private iterableDiffers: IterableDiffers,
        public dialog: MatDialog,
        private cd: ChangeDetectorRef
    ) {
        // For array input difference handling
        this.iterableDiffer = iterableDiffers.find([]).create(null);

        if (_data) {
            this.headers = _data.headers;
            this.simpleFieldLabel = _data.simpleFieldLabel;
            this.defaultTerm = _data.defaultTerm;
            this.inputArray = _data.inputArray;
            this.showFilterSaving = _data.showFilterSaving;
            this.savedFilters = _data.savedFilters;
        }
    }

    // ***********************************************************************************************************
    // Inputs and outputs
    // ***********************************************************************************************************

    /** Array of fields the user can set advance rules for */
    @Input() headers: NgAsHeader[] = null;
    /** Label for the basic search input field */
    @Input() simpleFieldLabel: string = "Search items";
    /** What search term to have applied by default */
    @Input() defaultTerm: NgAsSearchTerm = null;
    /** Array to apply the filters on */
    @Input() inputArray: any[] = null;
    /** Should the component enable filter saving UI */
    @Input() showFilterSaving: boolean = false;
    /** Terms saved by the user previously */
    @Input() savedFilters: NgAsSearchTerm[] = [];

    /** The search term the user set up */
    @Output() selectedTerm = new EventEmitter<NgAsSearchTerm>();
    /** The inputArray with the selectedTerm applied */
    @Output() outputArray = new EventEmitter<any[]>();
    /** The uppdated list of saved terms */
    @Output() savedFiltersChanged = new EventEmitter<NgAsSearchTerm[]>();

    // ***********************************************************************************************************
    // User configured search term
    // ***********************************************************************************************************

    /** The terms the user configured */
    searchTerm: NgAsSearchTerm = {
        searchMode: "advanced",
        simpleSearchTerm: "",
        advancedSearchType: "and",
        advancedTerms: [{ id: 0 }]
    };

    /** Counter that provides unique IDs for the advanced terms */
    advancedTermCounter = 1;

    // ***********************************************************************************************************
    // Animation helpers
    // ***********************************************************************************************************

    /** Component top-level DOM element */
    @ViewChild("container") container: ElementRef<HTMLElement>;

    /** Current height of the advanced term rows */
    public get advancedFieldHeight(): number {
        if (this.container === undefined) {
            return 0;
        }

        let val = 0;

        // New term row, constant
        val += 45;

        // Calculate height advance terms
        //    If the container is wide, use one-row-per-term value,
        //    if it is narrow, use two-row-per-term value
        const advancedTermCount = this.searchTerm.advancedTerms.length;
        const containerWidth = this.container.nativeElement.offsetWidth;
        val += advancedTermCount * (containerWidth > 749 ? 70 : 139);

        return val;
    }

    // ***********************************************************************************************************
    // Input handling
    // ***********************************************************************************************************

    ngOnInit(): void {
        // If no headers were proivded throw an error, since it is necessary for the advanced rule setup
        if (this.headers === null) {
            throw new Error("Input 'headers' is required");
        }

        var savedFilterString = localStorage.getItem("saved-filters");

        if (this.defaultTerm == null && savedFilterString) {
            this.defaultTerm = JSON.parse(savedFilterString);
        }

        // If a default term was provided apply it
        if (this.defaultTerm !== null) {
            this.searchTerm = this.defaultTerm;

            if (this.searchTerm.advancedTerms.length === 0) {
                this.searchTerm.advancedTerms.push({ id: 0 });
            }
        }

        // If saved terms were provided, check for a default there
        if (this.savedFilters.length !== 0) {
            this.savedFilters.forEach((f) => {
                if (f.advancedTerms.length === 0) {
                    f.advancedTerms.push({ id: 0 });
                }
            });

            const defFilter = this.savedFilters.find((f) => f.isDefault === true);
            if (defFilter !== undefined) {
                this.termLoaded(defFilter.name);
            }
        }

        // Set output
        this.outputUpdate();
    }

    ngAfterViewInit() {
        this.cd.detectChanges();
    }

    // ngDoCheck fails to detect array element changes by default. The following is a workaround
    iterableDiffer;

    ngDoCheck() {
        let changes = this.iterableDiffer.diff(this.inputArray);
        if (changes) {
            // Input array changed, update output
            this.outputUpdate();
        }
    }

    // ***********************************************************************************************************
    // Output handling
    // ***********************************************************************************************************

    /**  */
    savedTermsChanged() {
        this.savedFiltersChanged.emit(this.savedFilters);
    }

    /** Term or input changed, update output */
    outputUpdate() {
        this.selectedTerm.emit(this.searchTerm);
        this.updateOutputArray();
    }

    /** Apply the search terms to the inputArray, emit result */
    updateOutputArray() {
        // No usable input array was provided
        if (this.inputArray === null || this.inputArray.length === 0) {
            this.outputArray.emit(this.inputArray);
            return;
        }

        // Call appropriate fitler function
        let results: any[];
        if (this.searchTerm.searchMode === "simple") {
            results = this.simpleFilter();
        } else {
            results = this.advancedFilter();
        }

        // Emit result
        this.outputArray.emit(results);
    }

    /** Apply simpleSearchTerm on every property of an item */
    simpleFilter(): any[] {
        // Search term is empty, return unfiltered array
        if (this.searchTerm.simpleSearchTerm === "" || this.searchTerm === null) {
            return this.inputArray;
        }

        // Apply filter
        return this.inputArray.filter((item) =>
            // If at least one property includes the term, the item passes
            Object.keys(item).some((key) => String(item[key]).toLowerCase().includes(this.searchTerm.simpleSearchTerm.toLowerCase()))
        );
    }

    /** Apply the advancedTerms on the items */
    advancedFilter(): any[] {
        // Terms are invalid, return every item
        if (this.searchTerm.advancedTerms.every((t) => t.action === undefined)) {
            return this.inputArray;
        }

        // Apply filters
        return this.inputArray.filter((item) => {
            if (this.searchTerm.advancedSearchType === "and") {
                // AND mode, every rule needs to pass for an item
                return this.searchTerm.advancedTerms.every((term) => this.advancedTermPassed(item, term));
            } else {
                // OR mode, at least one rule needs to pass for an item
                return this.searchTerm.advancedTerms.some((term) => this.advancedTermPassed(item, term));
            }
        });
    }

    /** Test an advanced rule on an item */
    advancedTermPassed(item: any, term: NgAsAdvancedSearchTerm): boolean {
        if (term.action === undefined) {
            return true;
        }

        let rerturnVal: boolean = false;

        const evalValue = String(item[term.fieldName]).toLowerCase();
        const termValue = String(term.searchTerm).toLowerCase();

        // Test value based on selected criteria
        switch (term.action) {
            case "contains":
                rerturnVal = evalValue.includes(termValue);
                break;

            case "equals":
                rerturnVal = evalValue === termValue;
                break;

            case "larger than":
                rerturnVal = evalValue > termValue;
                break;

            case "smaller than":
                rerturnVal = evalValue < termValue;
                break;

            default:
                break;
        }

        return term.isNegated ? !rerturnVal : rerturnVal;
    }

    // ***********************************************************************************************************
    // Search term configuration
    // ***********************************************************************************************************

    /** Set the basic search term to empty */
    clearBasicTerm() {
        this.searchTerm.simpleSearchTerm = "";

        this.outputUpdate();
    }

    /** Toggle between basic and advanced search mode */
    toggleSearchMode() {
        if (this.searchTerm.searchMode === "simple") {
            this.searchTerm.searchMode = "advanced";
        } else {
            this.searchTerm.searchMode = "simple";
        }

        this.outputUpdate();
    }

    /** Toggle the link between advanced terms (AND/OR) */
    toggleAdvancedLink() {
        if (this.searchTerm.advancedSearchType === "and") {
            this.searchTerm.advancedSearchType = "or";
        } else {
            this.searchTerm.advancedSearchType = "and";
        }

        this.outputUpdate();
    }

    /** Negate an advanced search term */
    termNegate(term: NgAsAdvancedSearchTerm) {
        term.isNegated = false;

        this.outputUpdate();
    }

    /** Add a new advanced search term */
    addTermRow() {
        this.searchTerm.advancedTerms.push({ id: this.advancedTermCounter });
        this.advancedTermCounter++;

        this.outputUpdate();
    }

    /** Delete an advance search term */
    delTermRow(termId) {
        this.searchTerm.advancedTerms = this.searchTerm.advancedTerms.filter((t) => t.id !== termId);
        if (this.searchTerm.advancedTerms.length === 0) {
            this.searchTerm.advancedTerms.push({ id: 0 });
        }

        this.outputUpdate();
    }

    /** An advanced search terms column was changed */
    updateTermField(term: NgAsAdvancedSearchTerm) {
        // Populate term type based on header type
        const header = this.headers.find((h) => h.id === term.fieldName);
        term.fieldType = "text";
        if (header.type !== undefined) {
            term.fieldType = header.type;
        }
        if (term.fieldType == "checkbox") {
            term.searchTerm = 'true';
        }
        term.data = header.data;
        this.outputUpdate();
    }

    // ***********************************************************************************************************
    // Filter saving
    // ***********************************************************************************************************

    loadedFilterName: string = null;
    loadedFilter: NgAsSearchTerm = null;

    public get loadedFilterChanged(): boolean {
        return !this.areTermsEqual(this.loadedFilter, this.searchTerm);
    }

    openFilterSavePopup() {
        // const dRef = this.dialog.open(FilterSavePopupComponent, {
        //     data: {
        //         filters: this.savedFilters,
        //         loadedfilter: this.loadedFilterName,
        //         selectedIsValid: !this.loadedFilterChanged
        //     }
        // });
        // dRef.afterClosed().subscribe((success) => {
        //     if (success !== undefined) {
        //         if (success["selected"] !== undefined) {
        //             this.termLoaded(success["selected"]);
        //         }
        //         if (success["deleted"] !== undefined) {
        //             this.termDeleted(success["deleted"]);
        //         }
        //         if (success["name"] !== undefined) {
        //             this.termSaved(success);
        //         }
        //     }
        // });
    }

    termLoaded(name: string) {
        this.loadedFilterName = name;
        this.loadedFilter = this.savedFilters.find((f) => f.name === this.loadedFilterName);
        this.searchTerm = {
            simpleSearchTerm: this.loadedFilter.simpleSearchTerm,
            searchMode: this.loadedFilter.searchMode,
            advancedSearchType: this.loadedFilter.advancedSearchType,
            advancedTerms: this.loadedFilter.advancedTerms.map(
                (t) =>
                    ({
                        id: t.id,
                        fieldName: t.fieldName,
                        isNegated: t.isNegated,
                        action: t.action,
                        searchTerm: t.searchTerm
                    } as NgAsAdvancedSearchTerm)
            )
        } as NgAsSearchTerm;

        this.outputUpdate();
    }

    termSaved(data: { name: string; isDefault: boolean }) {
        if (data.isDefault === true) {
            this.savedFilters.filter((f) => f.isDefault === true).forEach((f) => (f.isDefault = false));
            this.searchTerm.isDefault = true;
        }

        let existingTerm = this.savedFilters.find((f) => f.name === data.name);

        if (existingTerm === undefined) {
            existingTerm = {} as NgAsSearchTerm;
            existingTerm.name = data.name;
            this.savedFilters.push(existingTerm);
        }

        existingTerm.simpleSearchTerm = this.searchTerm.simpleSearchTerm;
        existingTerm.searchMode = this.searchTerm.searchMode;
        existingTerm.advancedSearchType = this.searchTerm.advancedSearchType;
        existingTerm.advancedTerms = this.searchTerm.advancedTerms.map((t) => t);
        existingTerm.isDefault = data.isDefault;

        this.savedTermsChanged();
        this.termLoaded(data.name);
    }

    termDeleted(name: string) {
        this.savedFilters = this.savedFilters.filter((f) => f.name !== name);
        if (this.loadedFilterName === name) {
            this.loadedFilterName = null;
            this.loadedFilter = null;
        }

        this.savedTermsChanged();
    }

    areTermsEqual(a: NgAsSearchTerm, b: NgAsSearchTerm): boolean {
        if (a === null && b !== null) {
            return false;
        }
        if (a !== null && b === null) {
            return false;
        }
        if (a === null && b === null) {
            return true;
        }

        if (a.simpleSearchTerm !== b.simpleSearchTerm) {
            return false;
        }
        if (a.searchMode !== b.searchMode) {
            return false;
        }
        if (a.advancedSearchType !== b.advancedSearchType) {
            return false;
        }
        if (a.advancedTerms.length !== b.advancedTerms.length) {
            return false;
        }
        return a.advancedTerms.every((termA, index) => {
            const termB = b.advancedTerms[index];
            if (termA.id !== termB.id) {
                return false;
            }
            if (termA.fieldName !== termB.fieldName) {
                return false;
            }
            if (termA.isNegated !== termB.isNegated) {
                return false;
            }
            if (termA.action !== termB.action) {
                return false;
            }
            if (termA.searchTerm !== termB.searchTerm) {
                return false;
            }
            return true;
        });
    }

    applySearch() {
        if (this.searchTerm.advancedTerms[0].fieldName) {
            var searchTerm = JSON.parse(JSON.stringify(this.searchTerm));

            for (let index = 0; index < searchTerm.advancedTerms.length; index++) {
                var element = searchTerm.advancedTerms[index];
                if (element.fieldType == "date" && element.action == "=") {
                    var newObj = Object.assign({}, element);
                    element.action = ">=";
                    element.searchTerm = element.searchTerm + " 00:00:00";
                    newObj.action = "<=";
                    newObj.searchTerm = newObj.searchTerm + " 23:59:59";
                    searchTerm.advancedTerms.push(newObj);
                }
            }

            this.dialogRef.close(searchTerm);
            localStorage.setItem("saved-filters", JSON.stringify(this.searchTerm));
        } else {
            this.dialogRef.close(null);
            localStorage.removeItem("saved-filters");
        }
    }
    resetSearch() {
        this.dialogRef.close(null);
        localStorage.removeItem("saved-filters");
    }
}
