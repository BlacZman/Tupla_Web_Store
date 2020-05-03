# Tupla Web Store
How to run these thing?!
- Open project file
- In toolbar tab on the top, click Tools
- Go to 'NuGet Package Manager'
- Open 'Package Manager Console'
- In command-line, type "Update-Database -Context DataContext"
- Find the Default project, that's at the top in command-line window.
- select Default project to 'Tupla.Data.Context'
- In command-line, type "Update-Database -Context TuplaContext"
- Run the web server by pressing F5 or CTRL +  F5

- If you have problem, type in command-line from above, "Drop-Database -Context DataContext(or TuplaContext)" (CHOOSE ONE CONTEXT)
- Try again.
