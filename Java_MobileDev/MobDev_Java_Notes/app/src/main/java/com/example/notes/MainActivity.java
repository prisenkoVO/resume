package com.example.notes;

import android.app.AlertDialog;
import android.app.LoaderManager;
import android.content.ContentUris;
import android.content.CursorLoader;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.Loader;
import android.database.Cursor;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.ContextMenu;
import android.view.View;
import android.widget.Toast;

import com.example.notes.db.NotesContract;
import com.example.notes.ui.NoteImagesAdapter;
import com.example.notes.ui.NotesAdapter;

public class MainActivity extends AppCompatActivity implements LoaderManager.LoaderCallbacks<Cursor> {

    private NotesAdapter notesAdapter;
    private static final int CM_DELETE_ID = 1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        RecyclerView recyclerView = findViewById(R.id.notes_rv);
        LinearLayoutManager layoutManager = new LinearLayoutManager(this);
        recyclerView.setLayoutManager(layoutManager);

        notesAdapter = new NotesAdapter(null, onNoteClickListener, onLongClickListener);
        recyclerView.setAdapter(notesAdapter);


        getLoaderManager().initLoader(
                0, // Идентификатор загрузчика
                null, // Аргументы
                this // Callback для событий загрузчика
        );

        findViewById(R.id.create_fab).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this, CreateNoteActivity.class);
                startActivity(intent);
            }
        });
    }


    @Override
    public Loader<Cursor> onCreateLoader(int id, Bundle args) {
        return new CursorLoader(
                this,  // Контекст
                NotesContract.Notes.URI, // URI
                NotesContract.Notes.LIST_PROJECTION, // Столбцы
                null, // Параметры выборки
                null, // Аргументы выборки
                null // Сортировка по умолчанию
        );
    }

    @Override
    public void onLoadFinished(Loader<Cursor> loader, Cursor cursor) {
        Log.i("Test", "Load finished: " + cursor.getCount());

        cursor.setNotificationUri(getContentResolver(), NotesContract.Notes.URI);

        notesAdapter.swapCursor(cursor);
    }

    @Override
    public void onLoaderReset(Loader<Cursor> loader) {

    }

    /**
     * Listener для клика по заметке
     */
    private final NotesAdapter.OnNoteClickListener onNoteClickListener = new NotesAdapter.OnNoteClickListener() {
        @Override
        public void onNoteClick(long noteId) {
            Intent intent = new Intent(MainActivity.this, NoteActivity.class);
            intent.putExtra(NoteActivity.EXTRA_NOTE_ID, noteId);

            startActivity(intent);
        }
    };

    private void deleteNotes(long noteId) {
        getContentResolver().delete(ContentUris.withAppendedId(NotesContract.Notes.URI, noteId),
                null,
                null);
    }

    private final NotesAdapter.OnLongClickListener onLongClickListener=
            new NotesAdapter.OnLongClickListener() {
                @Override
                public void onLongClick(final long noteId) {
                    AlertDialog alertDialog = new AlertDialog.Builder(MainActivity.this)
                            .setMessage(R.string.message_delete_note)
                            .setPositiveButton(R.string.title_btn_yes, new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialog, int which) {
                                    deleteNotes(noteId);
                                }
                            })
                            .setNegativeButton(R.string.title_btn_no, null)
                            .create();

                    if (!isFinishing()) {
                        alertDialog.show();
                    }
                }
            };
}
