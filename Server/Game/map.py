class Map(object):

    id = None
    name = None
    _heights = None


    def __init__(self, id, heightmap=None, name=None):
        self.id = id
        self.name = name
        if heightmap: self.heights = heightmap

    def load(self, store):
        store.load_map(self)

    def save(self, store):
        store.save_map(self)



    def export_map(self):
        if not self._heights: return None

        height_string = str(len(self._heights[0]))

        for row in self._heights:
            for col in row:
                height_string = height_string + str(col)

        return height_string

    @property
    def heights(self):
        return self._heights

    @heights.setter
    def heights(self, heights):
        print "SETTING HEIGHTS"
        # Figure out what type of data we're dealing with
        if isinstance(heights[0], list):
            # This is a matrix of some sort. Good!
            self._heights = heights

        else:
            # This is serial (probably a string). It will need to be imported.

            # By convention, the first items will be width.
            w = int(heights[0])

            self._heights = [[]]

            i = 1
            y = 0

            while i < len(heights):

                self._heights[y].append(heights[i])

                i = i + 1

                # Is this the end of a row?
                if (i-1) % w == 0:
                    y = y + 1
                    self._heights.append([])



