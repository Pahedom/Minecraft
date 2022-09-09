using System.Collections.Generic;
using UnityEngine;

public class BlockPool : Debuggable
{
    private List<Block> blocks = new List<Block>();

    public Block Spawn(Block block)
    {
        Block newBlock = Find(block);

        if (newBlock != null)
        {
            blocks.Remove(newBlock);
            newBlock.gameObject.SetActive(true);   
        }
        else
        {
            newBlock = Instantiate(block, transform);
        }

        return newBlock;
    }

    public void Dispawn(Block block)
    {
        block.gameObject.SetActive(false);
        blocks.Add(block);
    }

    private Block Find(Block block)
    {
        foreach (var item in blocks)
        {
            if (block.displayName == item.displayName)
            {
                return item;
            }
        }

        return null;
    }
}