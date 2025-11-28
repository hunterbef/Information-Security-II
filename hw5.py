from PIL import Image

def encode(cPath, sPath, oPath):
    cover = Image.open(cPath)
    secret = Image.open(sPath).resize(cover.size)

    cPixels = cover.load()
    sPixels = secret.load()

    w, h = cover.size

    embed = Image.new("RGB", (w, h))
    ePixels = embed.load()

    for y in range(h):
        for x in range(w):
            cPixel = cPixels[x, y]
            sPixel = sPixels[x, y]
            newPixel = []

            for i in range(3):
                cVal = cPixel[i] & 0b11111110
                sVal = (sPixel[i] >> 7) & 0b00000001
                newVal = cVal | sVal
                newPixel.append(newVal)
            
            ePixels[x, y] = tuple(newPixel)
    
    embed.save(oPath)

def decode(ePath, oPath):
    embed = Image.open(ePath)
    ePixels = embed.load()

    w, h = embed.size
    secret = Image.new("RGB", (w, h))
    sPixels = secret.load()

    for y in range(h):
        for x in range(w):
            ePixel = ePixels[x, y]
            newPixel = []

            for i in range(3):
                sVal = (ePixel[i] & 0b00000001) * 255
                newPixel.append(sVal)
            
            sPixels[x, y] = tuple(newPixel)
    
    secret.save(oPath)


if __name__ == "__main__":
    encode("cover.png", "secret.png", "embed.png")
    decode("embed.png", "recover.png")