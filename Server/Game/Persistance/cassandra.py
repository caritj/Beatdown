
import cql

class CQLStorage:
    host = 'localhost'
    port = 9160
    keyspace = 'beatdown'
    cql_version = '3.0.0'

    connection = None

    def __init__(self):
        self.connection = cql.connect(self.host, self.port, self.keyspace,
                                      cql_version=self.cql_version)

    def load(self, obj):
        if isinstance(obj, Map): self.load_map(obj)

    def load_map(self, obj):
        cursor = self.connection.cursor()
        cursor.execute("SELECT name, heights FROM maps WHERE id=:id", dict(id=obj.id))

        height_string = None
        map_name = None

        for row in cursor:
            height_string = row[1]
            map_name = row[0]
        cursor.close()

        obj.heights = height_string
        obj.name = map_name


    def save(self, obj):
        if isinstance(obj, Map): self.save_map(obj)

    def save_map(self, obj):

        cursor = self.connection.cursor()

        # Is this a new map?
        cursor.execute("SELECT id FROM maps WHERE id=:id", dict(id=obj.id))
        new_map = cursor.rowcount <= 0

        if new_map:
            print ("INSERT INTO maps (id, heights, name) VALUES (:id, :heights, :name)",
                           {'id':obj.id, 'heights':obj.export_map(), 'name':obj.name})
            cursor.execute("INSERT INTO maps (id, heights, name) VALUES (:id, :heights, :name)",
                           {'id':obj.id, 'heights':obj.export_map(), 'name':obj.name}
                           )
        else:
            print("UPDATE maps SET heights=:heights, name=:name WHERE id=:id",
                           dict(id=obj.id, heights=obj.export_map(), name=obj.name)
                           )
            cursor.execute("UPDATE maps SET heights=:heights, name=:name WHERE id=:id",
                           dict(id=obj.id, heights=obj.export_map(), name=obj.name)
                           )
        cursor.close()


    def cleanup(self):
        self.connection.close()

class CQLStorageManager:

    def __enter__(self):

        self.cql_storage = CQLStorage()
        return self.cql_storage

    def __exit__(self, type, value, traceback):
        self.cql_storage.cleanup()






