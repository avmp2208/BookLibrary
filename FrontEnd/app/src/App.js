import './App.css';

import React, { useState, useEffect } from 'react';


const styles = {
  container: { padding: '20px', fontFamily: 'Arial, sans-serif' },
  input: { marginRight: '10px', padding: '10px', border: '1px solid #ccc', borderRadius: '5px' },
  button: { padding: '10px 20px', border: 'none', borderRadius: '5px', cursor: 'pointer', backgroundColor: '#007bff', color: '#ffffff' },
  select: { marginRight: '10px', padding: '10px', border: '1px solid #ccc', borderRadius: '5px' },
  table: { width: '100%', marginTop: '20px', borderCollapse: 'collapse' },
  th: { textAlign: 'left', padding: '10px', borderBottom: '1px solid #ddd' },
  td: { textAlign: 'left', padding: '10px', borderBottom: '1px solid #eee' },
  pagination: { marginTop: '20px', display: 'flex', justifyContent: 'center' },
  pageButton: { border: '1px solid #ccc', padding: '5px 10px', margin: '0 5px', cursor: 'pointer', backgroundColor: '#f0f0f0' }
};

function App() {
  const [searchValue, setSearchValue] = useState('');
  const [searchCategory, setSearchCategory] = useState('');
  const [books, setBooks] = useState([]);

  const fetchBooks = async () => {
    
    const response = await fetch(`http://localhost:5122/BookLibrary/GetBookList?searchBy=${searchCategory}&value=${searchValue}`);
    const data = await response.json();
    
    setBooks(data);
  };

  useEffect(() => {
    fetchBooks()
  }, [])

  return (
    <div style={styles.container}>
      <select
          value={searchCategory}
          onChange={(e) => setSearchCategory(e.target.value)}
          style={styles.select}
      >
        <option value="">Search by</option>
        <option value="own/love/want">own/love/want</option>
        <option value="author">author</option>
        <option value="isbn">isbn</option>
      </select>
      <input
          type="text"
          value={searchValue}
        onChange={(e) => setSearchValue(e.target.value)}
        placeholder="Search Value"
        style={styles.input}
      />
      <button onClick={fetchBooks} style={styles.button}>Search</button>
      <table style={styles.table}>
        <thead>
          <tr>
            <th style={styles.th}>Book title</th>
            <th style={styles.th}>Authors</th>
            <th style={styles.th}>Type</th>
            <th style={styles.th}>ISBN</th>
            <th style={styles.th}>Category</th>
            <th style={styles.th}>Available copies</th>
          </tr>
        </thead>
        <tbody>
          {books.map((book) => (
            <tr key={book.ISBN}>
              <td style={styles.td}>{book.title}</td>
              <td style={styles.td}>{book.firstName}  {book.lastName}</td>
              <td style={styles.td}>{book.type}</td>
              <td style={styles.td}>{book.isbn}</td>
              <td style={styles.td}>{book.category}</td>
              <td style={styles.td}>{book.totalCopies - book.copiesInUse}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;
