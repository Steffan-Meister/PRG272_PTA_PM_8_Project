# Baobab Ridge Wildlife Rehabilitation Records System

A Windows Forms desktop application (.NET Framework 4.7.2) for recording and
managing wildlife admitted to the Baobab Ridge Rehabilitation centre.

## Group members

| Name | Student number |
|------|----------------|
| _&lt;Member 1 name&gt;_ | _&lt;student number&gt;_ |
| _&lt;Member 2 name&gt;_ | _&lt;student number&gt;_ |
| _&lt;Member 3 name&gt;_ | _&lt;student number&gt;_ |

> Fill in the member names and student numbers before submitting.

## How to build and run

1. Install **Visual Studio 2019/2022** with the **.NET desktop development**
   workload (.NET Framework 4.7.2).
2. Clone the repository.
3. Open `Baobab Ridge Wildlife Rehabilitation Records System.slnx` (or the
   `.csproj`) in Visual Studio.
4. Build the solution (**Build → Build Solution**, or `Ctrl+Shift+B`).
5. Run it (**Debug → Start Without Debugging**, or `Ctrl+F5`).

From the command line:

```
dotnet build "Baobab Ridge Wildlife Rehabilitation Records System.csproj"
```

The program creates `animals.txt` in the application folder
(`bin\Debug\`) automatically on first run, so there are no hard-coded paths.

## Features

| # | Feature | How to use it |
|---|---------|---------------|
| 1 | **Add a new animal** | Fill in Animal ID, Name, Species, Age and Recovery Score, then click **Add**. Every field is validated; the status and housing unit are calculated and appended as one new line. |
| 2 | **View all animals** | The grid loads every record when the program starts and after each change. It is read-only, full-row-select and in file order. |
| 3 | **Update an animal** | Click a grid row, or type an ID in the **Search Animal ID** box and click **Search**. The record loads into the input boxes and the form switches to edit mode (ID read-only, Add disabled, **Save Changes** enabled). **Clear** returns to Add mode. |
| 4 | **Delete an animal** | Select a row and click **Delete**, then confirm. One record is deleted at a time. |
| 5 | **Generate a summary** | Click **Generate Summary**. Totals are shown on the summary form and saved to `summary.txt`. |

## File formats

**animals.txt** – one pipe-delimited line per animal, with a header row:

```
AnimalID|Name|Species|Age|RecoveryScore|Status|HousingUnit
WR-0001|Naledi|Lion|5|85|Ready for Release|Mammal Enclosure
```

**summary.txt** – the generated report (assumed Section 5.2 layout):

```
Baobab Ridge Wildlife Rehabilitation Records System
Summary Report
Generated: 2026-10-09 08:30:00

Total animals: 2
Average age: 2.50
Average recovery score: 32.50

Animals per status:
Critical: 1
Stable: 0
Recovering: 1
Improving: 0
Ready for Release: 0
```

## Assumed rules

Sections 3 (validation), 4 (classification) and 5.2 (summary layout) were not
available, so sensible defaults were used. They are all in one place,
`BusinessLogicLayer/BusinessLogicLayer.cs`, and are easy to change.

**Validation**

| Field | Rule |
|-------|------|
| Animal ID | required; unique (uniqueness is skipped on update) |
| Name | required |
| Species | required |
| Age | whole number between 0 and 100 |
| Recovery Score | whole number between 0 and 100 |

**Classification** – the single `Classify` method maps the score to a status and
the status + species to a housing unit:

| Recovery Score | Status |
|----------------|--------|
| 0–20 | Critical |
| 21–40 | Stable |
| 41–60 | Recovering |
| 61–80 | Improving |
| 81–100 | Ready for Release |

| Condition | Housing unit |
|-----------|--------------|
| Status is Critical | Intensive Care Unit |
| Species looks like a bird | Aviary |
| Species looks like a reptile | Reptile Habitat |
| Species looks like a mammal | Mammal Enclosure |
| Anything else | General Enclosure |

## Error handling

All file reads, appends, rewrites and the summary write are wrapped in
`DataHandler`. Missing files/folders, files in use (`IOException`) and
permission problems (`UnauthorizedAccessException`) are shown as a friendly
message and the program keeps running. Test by setting `animals.txt` to
read-only or opening it in another program while the app is running.

## Known limitations

- The validation, classification and summary layouts are **assumed** because the
  official Section 3, 4 and 5.2 rules were not supplied.
- Species-to-housing matching uses keywords; an unusual species name falls back
  to "General Enclosure".
- The summary report is displayed on a separate form.
- Data is stored in plain pipe-delimited text; there is no database.
