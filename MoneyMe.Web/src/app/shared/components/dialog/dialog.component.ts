import { Component, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

interface DialogData { 
  title: string;
  message: string;
  showCancelButton: boolean; 
}

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  standalone: false,
})
export class DialogComponent {
  constructor(private dialogRef: MatDialogRef<DialogComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {}

  onCancel(): void {
    this.dialogRef.close({ event: 'cancel' });
  }

  onConfirm(): void {
    this.dialogRef.close({ event: 'confirm' });
  }
}
